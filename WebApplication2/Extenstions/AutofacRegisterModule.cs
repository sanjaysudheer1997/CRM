using Autofac;
using Autofac.Extras.DynamicProxy;
using Core;
using CRMAPI.Repositories;
using CRMAPI.Services;
using OnlineMagic.Common.Core.Extensions;

namespace CRMAPI.Extenstions;

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
   // string identityDbConnectionString = configuration.GetConnectionString("IdentityDBConnection");

    //Register NpgsqlConnection with Autofac using the connection string.
   builder.Register(c => new NpgsqlConnection(coreDbConnectionString))
          .As<NpgsqlConnection>()
          .InstancePerLifetimeScope();

   // Register the second IdentityDBContext with a name
    //builder.RegisterType<NpgsqlConnection>()
    //       .Named<NpgsqlConnection>("IdentityDBContext")
    //       .WithParameter(new TypedParameter(typeof(string), identityDbConnectionString));



    //Register Repositories
    builder.RegisterAssemblyTypes(typeof(IEmployeeService).Assembly)
          .Where(t => t.IsAssignableTo<IBaseRepository>())
          .AsImplementedInterfaces()
          .InstancePerLifetimeScope()
          .EnableInterfaceInterceptors()
          .InterceptedBy(typeof(LoggingInterceptor))
          .PropertiesAutowired();

      //Register Services
      builder.RegisterAssemblyTypes(typeof(IEmployeeRepository).Assembly)
          .Where(t => t.IsAssignableTo<IBaseService>())
          .AsImplementedInterfaces()
          .InstancePerLifetimeScope()
          .EnableInterfaceInterceptors()
          .InterceptedBy(typeof(LoggingInterceptor))
          .PropertiesAutowired();

    builder.RegisterFrameworkComponents();

    //Register framework components
    base.Load(builder);
  }

}

