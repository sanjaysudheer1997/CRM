﻿using OnlineMagic.Common.Core.Extensions;
namespace Core.Repositories;

public abstract class Repository<T> : IRepository<T> where T : Model
{
  private readonly IHttpContextAccessor httpContextAccessor;

  private readonly NpgsqlConnection connection;
  private readonly QueryFactory queryFactory;
  protected string TableName => GetTableName(typeof(T));

  public Repository(
      IHttpContextAccessor httpContextAccessor,
      NpgsqlConnection connection)
  {
    this.httpContextAccessor = httpContextAccessor;
    this.connection = connection;

    queryFactory = new QueryFactory(connection, new PostgresCompiler());
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await queryFactory
        .Query<T>()
        .WhereNotDeleted()
        .GetAsync<T>();
  }

  public async Task<T> GetByIdAsync(long id)
  {
    return await queryFactory
        .Query<T>()
        .Where("id", id)
        .FirstOrDefaultAsync<T>();
  }

  public async Task<int> InsertAsync(T entity)
  {
    InsertDefaults(ref entity);

    return await queryFactory
        .Query<T>()
        .InsertGetIdAsync<int>(entity);
  }

  public async Task<int> UpdateAsync(long id, T entity)
  {
    UpdateDefaults(ref entity);

    return await queryFactory
        .Query<T>()
        .Where("id", id)
        .UpdateAsync(entity);
  }

  public async Task<int> DeleteAsync(long id)
  {
    //soft delete
    return await queryFactory
        .Query<T>()
        .Where("id", id)
        .UpdateAsync(new
        {
          isarchived = true
        });
  }

  public async Task<int> DeletePermanentAsync(long id)
  {
    //hard delete
    return await queryFactory
        .Query<T>()
        .Where("id", id)
        .DeleteAsync();
  }

  public async Task<int> GetCountAsync()
  {
    return await queryFactory
        .Query<T>()
        .WhereNotDeleted()
        .CountAsync<int>();
  }

  public async Task<DataTablesResponse<T>> GetPagedDataAsync(DataTablesRequest dataTablesRequest)
  {
    var query = new Query(TableName).WhereNotDeleted();

    // Get the searchable properties using reflection
    var searchableProperties = typeof(T).GetProperties()
        .Where(prop => prop.GetCustomAttribute<SearchableAttribute>() != null)
        .Select(prop => new
        {
          PropertyName = prop.Name,
          ColumnName = prop.GetCustomAttribute<SqlKata.ColumnAttribute>()?.Name ?? prop.Name
        });

    // Handle search
    if (!string.IsNullOrEmpty(dataTablesRequest.Search?.Value) && searchableProperties.Any())
    {
      foreach (var prop in searchableProperties)
      {
        query.OrWhereLike(prop.ColumnName, $"%{dataTablesRequest.Search.Value}%");
      }
    }

    // Get the total count before applying pagination
    int totalCount = await GetCountBeforePagination(query);

    // Handle sorting
    query = ApplySorting(dataTablesRequest, query);
    query = ApplyPagination(dataTablesRequest, query);

    // Get the paged data
    var data = await queryFactory.FromQuery(query).GetAsync<T>();

    // For simplicity, we'll assume the filtered count is equal to the total count
    var filteredCount = totalCount;

    return new DataTablesResponse<T>(dataTablesRequest.Draw, totalCount, filteredCount, data.ToList());
  }

  protected async Task<int> GetCountBeforePagination(Query query)
  {
    return await queryFactory.FromQuery(query.Clone().AsCount()).FirstAsync<int>();
  }

  protected Query ApplyPagination(DataTablesRequest dataTablesRequest, Query query)
  {
    return query.ForPage(dataTablesRequest.Start / dataTablesRequest.Length + 1, dataTablesRequest.Length);
  }

  protected Query ApplySorting(DataTablesRequest dataTablesRequest, Query query)
  {
    if (dataTablesRequest.Order != null && dataTablesRequest.Order.Count > 0)
    {
      var order = dataTablesRequest.Order.First(); // Get the first sorting column
      var columnName = dataTablesRequest.Columns[order.Column].Data;

      if (!string.IsNullOrEmpty(columnName))
      {
        if (order.Dir == "asc")
        {
          query.OrderBy(GetColumnName(columnName));
        }
        else
        {
          query.OrderBy(GetColumnName(columnName));
        }
      }
    }

    return query;
  }

  protected QueryFactory QueryFactory { get { return queryFactory; } }

  protected string GetColumnName(string propertyName)
  {
    PropertyInfo propertyInfo = typeof(T).GetProperties()
        .FirstOrDefault(p => p.GetCustomAttribute<SqlKata.ColumnAttribute>() != null);

    return propertyInfo.Name;
  }

  protected string GetTableName(Type type)
  {
    return "public." + type.Name.ToLower();
  }

  protected void InsertDefaults(ref T model)
  {
    //TODO: Change this to login user
    model.CreatedBy = model.ModifiedBy = "system";
  }

  protected void UpdateDefaults(ref T model)
  {
    //TODO: Change this to login user
    model.ModifiedBy = "system";
  }
}

