using Chat.Persistence.Dependencies.IServices;
using Chat.Persistence.Dependencies.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Persistence.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration)
    {
        // mongo
        services.AddSingleton<MongoService>();
        
        // services
        services.AddScoped<IChannelService, ChannelService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IServerService, ServerService>();
        services.AddScoped<IUserService, UserService>();


        return services;
    }
    
}