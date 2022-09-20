using Microsoft.AspNetCore.Http;
using RedisClassLibrary.Services;
using RedisClassLibrary.Services.Connection;

namespace RedisClassLibrary.Middlewares
{
    public class RedisConnectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RedisConService _conService;

        public RedisConnectionMiddleware(RequestDelegate next, RedisConService conService)
        {
            _next = next;
            _conService = conService;
        }

        public async Task Invoke(HttpContext context)
        {
            _conService.Connect();
            await _next(context);
        }
    }
}
