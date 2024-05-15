using CRM.Common.Core.Models;
using CRM.Common.Core.Repositories;

namespace CRM.Common.Core.Services;

public abstract class Service<T> : IService<T>
{
  public IRepository<T> Repository { get; set; }

  public async Task<int> DeleteAsync(long id)
  {
    return await Repository.DeleteAsync(id);
  }

  public async Task<int> DeletePermanentAsync(long id)
  {
    return await Repository.DeletePermanentAsync(id);
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await Repository.GetAllAsync();
  }

  public async Task<T> GetByIdAsync(long id)
  {
    return await Repository.GetByIdAsync(id);
  }

  public async Task<int> GetCountAsync()
  {
    return await Repository.GetCountAsync();
  }

  public async Task<int> InsertAsync(T entity)
  {
    return await Repository.InsertAsync(entity);
  }

  public async Task<int> UpdateAsync(long id, T entity)
  {
    return await Repository.UpdateAsync(id, entity);
  }

  async Task<DataTablesResponse<T>> IService<T>.GetPagedDataAsync(DataTablesRequest dataTablesRequest)
  {
    return await Repository.GetPagedDataAsync(dataTablesRequest);
  }
}


