
using CRMAPI.Repositories;

namespace CRMAPI.Services;

public class EmployeeService : Service<Employee>, IEmployeeService
{
  private readonly IEmployeeRepository _employeeRepository;

  public EmployeeService(IEmployeeRepository employeeRepository)
  {
    _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
  }
  public Task<int> CreateAsync(Employee employee)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
  {
    return await _employeeRepository.GetAllEmployeesAsync();
  }
}
