/*
 * Author: Karthikeyan
 * Date:   September 15, 2023
 * 
 * Description: Framework registration
 * 
 * Copyright (c) 2023 OnlineMagic. All rights reserved.
 */

using Core;
using Core.Repositories;
using Core.Services;

namespace OnlineMagic.Common.Core.Extensions;

public static class AutofacRegisterExtensions
{
    public static void RegisterFrameworkComponents(this ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterType<LoggingInterceptor>();
        builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
    }
}
