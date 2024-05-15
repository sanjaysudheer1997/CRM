using Core.Models;

namespace Core.Services;

public interface IService<T> : IBaseService
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T> GetByIdAsync(long id);
  Task<int> InsertAsync(T entity);
  Task<int> UpdateAsync(long id, T entity);
  Task<int> DeleteAsync(long id);
  Task<int> DeletePermanentAsync(long id);
  Task<int> GetCountAsync();
  Task<DataTablesResponse<T>> GetPagedDataAsync(DataTablesRequest dataTablesRequest);
}

public interface IBaseService { }