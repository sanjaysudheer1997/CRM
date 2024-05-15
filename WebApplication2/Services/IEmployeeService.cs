namespace CRMAPI.Services;

public interface IEmployeeService : IService<Employee>
{
  Task<int> CreateAsync(Employee employee);
  Task<IEnumerable<Employee>> GetAllEmployeesAsync();
}
