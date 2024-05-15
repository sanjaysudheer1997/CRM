using CRM.Common.Core.Repositories;
using Npgsql;
using SqlKata.Execution;

namespace CRM.Customer.Api.Repositories.Customer;

public class CustomerRepository : RepositoryEx<Common.Models.Customer>, ICustomerRepository
{
  public CustomerRepository(NpgsqlConnection connection) : base(connection)
  {
  }

  public async Task<IEnumerable<Common.Models.Customer>> GetAllCustomers()
  {
    var query = QueryFactory.Query("customer")
      .Select("*");

    var result = await QueryFactory.FromQuery(query).GetAsync<Common.Models.Customer>();

    return result.ToList();
  }
}
