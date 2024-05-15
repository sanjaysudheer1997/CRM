using CRM.Common.Core.Repositories;

namespace CRM.Common.Core.Extensions;

public static class AutofacRegisterExtensions
{
  public static void RegisterFrameworkComponents(this ContainerBuilder builder)
  {
    builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    builder.RegisterType<LoggingInterceptor>();
    //builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
  }
}
