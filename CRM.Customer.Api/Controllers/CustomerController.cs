using CRM.Customer.Api.Services.Customer;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Customer.Api.Controllers;

[ApiController]
[Route("api/customer/[controller]/[action]")]
public class CustomerController : ControllerBase
{
  private readonly ICustomerService customerService;
  public CustomerController(ICustomerService customerService)

  {
    this.customerService = customerService;
  }

  [HttpGet]
  public async Task<ActionResult<Common.Models.Customer>> GetAllCustomers()
  {
    return Ok(await customerService.GetAllCustomers());
  }
}
