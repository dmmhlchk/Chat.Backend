using Chat.API.Hub;

namespace Chat.API.Extensions.Middlewares;

public static class Middleware
{
    public static IApplicationBuilder UseApi(
        this IApplicationBuilder builder)
    {
        builder.UseHttpsRedirection();
        builder.UseRouting();
        
        
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<CommunicationHub>("/chat");
            endpoints.MapControllers();
        });

        
        return builder;
    }
}