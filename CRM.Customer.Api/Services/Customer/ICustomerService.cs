using CRM.Common.Core.Services;

namespace CRM.Customer.Api.Services.Customer;

public interface ICustomerService : IService<Common.Models.Customer>
{
  Task<IEnumerable<Common.Models.Customer>> GetAllCustomers();
}
