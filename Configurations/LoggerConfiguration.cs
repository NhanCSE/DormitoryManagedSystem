using LoggerService.Configurations;

public static class ConfigureLogger {
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>() ;
}