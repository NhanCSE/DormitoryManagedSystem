

public static class CorsConfiguration {
    public static IServiceCollection ConfigureCors(this IServiceCollection services) {

        services.AddCors(options => {
            options.AddPolicy("AllowSpecificOrigins", 
                builder => {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .WithMethods("GET", "PUT", "POST", "PATCH", "DELETE");
                }
            );
        });

        return services;
    }
}