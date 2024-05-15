using Autofac;
using CRM.Common.Core.Repositories;
using CRM.Common.Core.Services;
using CRM.Common.Core;
using CRM.Customer.Api.Controllers;
using Autofac.Extras.DynamicProxy;
using CRM.Customer.Api.Services.Customer;
using CRM.Customer.Api.Repositories.Customer;

namespace CRM.Customer.Api.Extensions;

public static class AutofacRegisterExtensions
{
  public static void RegisterCustomer(this ContainerBuilder builder)
  {
    //Register Repositories
    builder.RegisterAssemblyTypes(typeof(CustomerController).Assembly)
        .Where(t => t.IsAssignableTo<IBaseRepository>())
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope()
        .EnableInterfaceInterceptors()
        .InterceptedBy(typeof(LoggingInterceptor))
        .PropertiesAutowired();

    builder.RegisterAssemblyTypes(typeof(CustomerRepository).Assembly)
        .Where(t => t.IsAssignableTo<IBaseRepository>())
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope()
        .EnableInterfaceInterceptors()
        .InterceptedBy(typeof(LoggingInterceptor))
        .PropertiesAutowired();

    //Register Services
    //Register Services
    builder.RegisterAssemblyTypes(typeof(CustomerService).Assembly)
        .Where(t => t.IsAssignableTo<IBaseService>())
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope()
        .EnableInterfaceInterceptors()
        .InterceptedBy(typeof(LoggingInterceptor))
        .PropertiesAutowired();
  }
}
