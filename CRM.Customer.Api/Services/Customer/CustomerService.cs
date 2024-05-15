using CRM.Common.Core.Services;
using CRM.Customer.Api.Repositories.Customer;

namespace CRM.Customer.Api.Services.Customer;

public class CustomerService : Service<Common.Models.Customer>, ICustomerService
{
  private readonly ICustomerRepository customerRepository;
   public CustomerService(ICustomerRepository customerRepository)
  {
    this.customerRepository = customerRepository;
  }

  public async Task<IEnumerable<Common.Models.Customer>> GetAllCustomers()
  {
    return await customerRepository.GetAllCustomers();
  }
}
