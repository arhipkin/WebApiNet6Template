using MicroserviceTemplate.Settings;

namespace MicroserviceTemplate.Configuration
{
    public static class ApplicationSettings
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WeatherForecast>(configuration.GetSection(nameof(WeatherForecast)));
        }
    }
}
