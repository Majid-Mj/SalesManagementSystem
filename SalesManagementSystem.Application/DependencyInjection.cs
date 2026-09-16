using Microsoft.Extensions.DependencyInjection;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Application.Services;

namespace SalesManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();

        return services;
    }
}
