using CRMAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
public class EmployeeController : ControllerBase
{
  private readonly IEmployeeService employeeService;  
  public EmployeeController(IEmployeeService employeeService)
  {
    this.employeeService = employeeService; 
  }

  [HttpGet]
  public async Task<IActionResult> GetAllEmployees()
  {
    var employees = await employeeService.GetAllEmployeesAsync();

    return Ok(employees);
  }
}
