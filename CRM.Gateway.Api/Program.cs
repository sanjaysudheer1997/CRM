using Autofac;
using Autofac.Extensions.DependencyInjection;
using CRM.Common.Core.Exceptions;
using CRM.Common.Core.Extensions;
using CRM.Customer.Api.Controllers;
using CRM.Gateway.Api.Extensions;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//configure additional configuraiton file(s) - serilog
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
    .AddJsonFile("logsettings.json", optional: true, reloadOnChange: true)
    .Build();

// Configure Serilog using the configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();

//Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
    builder => builder.RegisterModule(new AutofacRegisterModule(configuration)));

//Add weatherforecast controller.
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(CustomerController).Assembly)//Authentication
    .AddNewtonsoftJson();


// Add session services
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(60); // You can set your timeout
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
  opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
  opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = JwtBearerDefaults.AuthenticationScheme
  });
  opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});



// Configure Dapper column name mapping
DefaultTypeMap.MatchNamesWithUnderscores = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
//TODO : remove this comment in final live
//if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Call your static class's Configure method here
app.Use((context, next) =>
{
  HttpHelper.Configure(context);
  return next.Invoke();
});

//allow to use session
//app.UseSession();

//Error handler middleware
app.UseMiddleware(typeof(ApiExceptionMiddleware));
//app.UseMiddleware(typeof(JwtMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

