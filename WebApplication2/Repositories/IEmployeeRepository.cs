using Core.Repositories;

namespace CRMAPI.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
  Task<IEnumerable<Employee>> GetAllEmployeesAsync();
  Task<Employee> GetEmployeeAsync(long id);
}
