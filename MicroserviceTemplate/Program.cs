using Autofac;
using Autofac.Extensions.DependencyInjection;
using MicroserviceTemplate.Configuration;
using MicroserviceTemplate.Configuration.Autofac;
using MicroserviceTemplate.Filters;
using MicroserviceTemplate.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Register Autofac as DI service provider
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new AppModule());
        });

    JsonConvert.DefaultSettings = () =>
    {
        var settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = builder.Environment.IsDevelopment() ? Formatting.Indented : Formatting.None
        };
        settings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
        return settings;
    };

    builder.Services.AddCors();

    builder.Services.AddOptions();
    builder.Services.AddSettings(builder.Configuration);

    builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelFilter>();
            options.Filters.Add<CacheControlFilter>();
        }).AddNewtonsoftJson();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    builder.Services.AddAutoMapper(typeof(Program));

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseAppExceptionHandler();
    app.UseOptionsVerbHandler();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();    

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseCors(builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)
    );

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "An unhandled exception occurred. The application will be closed");
}
finally
{
    Log.Information("Shut down complete...");
    Log.CloseAndFlush();
}