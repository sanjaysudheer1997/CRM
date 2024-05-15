using CRM.Common.Core.Repositories;

namespace CRM.Customer.Api.Repositories.Customer;

public interface ICustomerRepository : IRepository<Common.Models.Customer>
{
  Task<IEnumerable<Common.Models.Customer>> GetAllCustomers();
}
