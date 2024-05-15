using Autofac;
using CRM.Common.Core.Extensions;
using CRM.Customer.Api.Extensions;
using Npgsql;

namespace CRM.Gateway.Api.Extensions;

public class AutofacRegisterModule : Autofac.Module
{
  private readonly IConfiguration configuration;

  public AutofacRegisterModule(IConfiguration configuration)
  {
    this.configuration = configuration;
  }

  protected override void Load(ContainerBuilder builder)
  {
    // Read the connection string from appsettings.json.
    string coreDbConnectionString = configuration.GetConnectionString("CoreDBConnection");

    // Read the connection string from appsettings.json.
    string identityDbConnectionString = configuration.GetConnectionString("IdentityDBConnection");

    // Register NpgsqlConnection with Autofac using the connection string.
    builder.Register(c => new NpgsqlConnection(coreDbConnectionString))
           .As<NpgsqlConnection>()
           .InstancePerLifetimeScope();

    // Register the second IdentityDBContext with a name
    builder.RegisterType<NpgsqlConnection>()
           .Named<NpgsqlConnection>("IdentityDBContext")
           .WithParameter(new TypedParameter(typeof(string), identityDbConnectionString));

    builder.RegisterFrameworkComponents();

    builder.RegisterCustomer();

    base.Load(builder);
  }
}

