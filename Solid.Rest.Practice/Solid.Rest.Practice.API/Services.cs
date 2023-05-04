using Solid.Rest.Practice.API.BLL;
using Solid.Rest.Practice.API.DAL;

namespace Solid.Rest.Practice.API;

public static class Services
{
    // Add services to the container.
    public static void ServiceRegistrations(WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddSingleton<ISuperSimpleMemoryDatabase, SuperSimpleMemoryDatabase>();
        webApplicationBuilder.Services.AddSingleton<ICounterService, CounterService>();
        webApplicationBuilder.Services.AddTransient<ICustomerDatabase, CustomerDatabaseBasedOnSuperSimpleMemoryDatabase>();
        webApplicationBuilder.Services.AddTransient<IDateProvider, DateProvider>();
        webApplicationBuilder.Services.AddTransient<ICustomerService, CustomerService>();
    }
}