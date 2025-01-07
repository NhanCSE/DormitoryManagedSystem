
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);
//Environment
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var configFile = environment == "Production" ? "nLog.Production.config" : "nLog.Development.config";
 
LogManager.LoadConfiguration(
    Path.Combine(
        Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, 
        "LoggerService", 
        "Configurations", 
        configFile
    )
);


// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureISSIntegration();
builder.Services.ConfigureLoggerService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowSpecificOrigins");


//Set in lauchSetting in ASPNETCORE_ENVIRONMENT, 3 option: "Development", "Staging", "Production"

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
} else {
    app.UseHsts();
}

app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseHttpsRedirection();

app.UseAuthorization();

// app.Use(async (context, next) => {
//     Console.WriteLine("Start Middleware");
//     await next.Invoke();
//     Console.WriteLine("End Middleware");
// }); 

app.Map("/mapchecking", builder => {
    builder.Use(async (context, next) => {
        Console.WriteLine("Map checking begin");
        await next.Invoke();
        Console.WriteLine("End Mapchecking");

    });
    builder.MapWhen(context => context.Request.Query.ContainsKey("mapwhen"), nestedBuilder => {
        nestedBuilder.Run(async context => {
            Console.WriteLine("MapWhen checking inside /mapchecking");
            await context.Response.WriteAsync("Hello from nested MapWhen");
        });
    });
    builder.Run(async context => {
        string method = "Run";
        Console.WriteLine($"Method runing is {method}");
        await context.Response.WriteAsync("Hello from Map Checking");
    });     
});

// app.MapWhen(context => context.Request.Query.ContainsKey("mapwhen"), builder => {
//     builder.Run(async context => {
//         Console.WriteLine("MapWhen checking");
//         await context.Response.WriteAsync("Hello from MapWhen");
//     });
// });

// app.Run(async context => {
//     context.Response.StatusCode = 200;
//     await context.Response.WriteAsync("MiddleWare checking!");
// });

app.MapControllers();

app.Run();
