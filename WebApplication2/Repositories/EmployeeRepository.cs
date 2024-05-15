

using SqlKata.Execution;

namespace CRMAPI.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
  public EmployeeRepository(IHttpContextAccessor httpContextAccessor, NpgsqlConnection connection) : base(httpContextAccessor, connection)
  {
  }

  public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
  {
    var result =  await QueryFactory.Query("employee")
      .Select("*")
      .GetAsync<Employee>();

    return result;
  }

  public Task<Employee> GetEmployeeAsync(long id)
  {
    throw new NotImplementedException();
  }
}
