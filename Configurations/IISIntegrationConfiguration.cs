

public static class IISConfiguration {
    public static IServiceCollection ConfigureISSIntegration(this IServiceCollection services) {
        services.Configure<IISOptions>(options => {
            
        });
        return services;
    }
}