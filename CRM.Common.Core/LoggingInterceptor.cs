/*
 * Author: Karthikeyan
 * Date:   September 15, 2023
 * 
 * Description: Loggin Interceptor
 * 
 * Copyright (c) 2023 OnlineMagic. All rights reserved.
 */

using Castle.DynamicProxy;

namespace CRM.Common.Core;

public class LoggingInterceptor : IInterceptor
{
  private readonly ILogger<LoggingInterceptor> logger;

  public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
  {
    this.logger = logger;
  }

  public void Intercept(IInvocation invocation)
  {
    //TODO optimize for the production.
    System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

    logger.LogInformation($">> {invocation.TargetType.FullName}.{invocation.Method.Name}");

    // Invoke the method
    invocation.Proceed();

    watch.Stop();

    logger.LogInformation($"<< {invocation.TargetType.FullName}.{invocation.Method.Name} :: {watch.ElapsedMilliseconds} ms");
  }
}

