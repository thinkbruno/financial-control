using FinancialControl.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialControl.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMessaging(
        this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher>();

        return services;
    }
}