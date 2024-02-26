namespace Chat.API.Extensions.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR();
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        

        return services;
    }
}