using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace APIConfig.Class
{
    public class ApiKeyMiddleware
    {
        private readonly IConfiguration Configuration;
        private readonly RequestDelegate _next;
        private const string API_KEY = "ApiKey";  // ใช้ชื่อ header สำหรับ API Key
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            Configuration = configuration;
            _apiKey = configuration.GetValue<string>("ApiKey");  // ดึง API Key จาก configuration
        }
    
        public async Task InvokeAsync(HttpContext context)
        {
    if (!context.Request.Headers.TryGetValue(API_KEY, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key is missing");
            return;
        }

        if (!_apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized client");
            return;
        }

        await _next(context);
        }
    }
}
