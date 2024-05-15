using CRM.Common.Core.Models;

namespace CRM.Common.Core.Repositories;

public interface IRepository<T> : IBaseRepository
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

public interface IBaseRepository { }