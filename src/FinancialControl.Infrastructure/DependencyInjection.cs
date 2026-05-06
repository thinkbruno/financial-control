using FinancialControl.Application.Interfaces;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Infrastructure.Messaging;
using FinancialControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialControl.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<IEventPublisher, EventPublisher>();

        return services;
    }
}