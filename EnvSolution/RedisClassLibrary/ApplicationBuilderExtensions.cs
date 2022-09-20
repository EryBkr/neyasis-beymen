using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RedisClassLibrary.Abstract;
using RedisClassLibrary.Middlewares;
using RedisClassLibrary.Services.Concrete;
using RedisClassLibrary.Services.Connection;

namespace RedisClassLibrary
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddRedisMiddleware(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<RedisConnectionMiddleware>();

            return appBuilder;
        }

        public static IServiceCollection AddRedisService(this IServiceCollection services, Action<Options> optionAction)
        {
            var opt = new Options();
            optionAction(opt);

            services.AddSingleton<IEnvService, EnvService>(sp =>
            {
                var redisCon = sp.GetRequiredService<RedisConService>();
                return new EnvService(redisCon, opt);
            });
            services.AddSingleton<RedisConService>();

            return services;
        }
    }
}
