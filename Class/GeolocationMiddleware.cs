using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Interface;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;

namespace APIConfig.Class
{
    
   public class GeolocationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IBlockedIpRepository _blockedIpRepository;
    private readonly string _geoLite2DbPath;

    public GeolocationMiddleware(RequestDelegate next, IBlockedIpRepository blockedIpRepository, IWebHostEnvironment env)
    {
        _next = next;
        _blockedIpRepository = blockedIpRepository;
        _geoLite2DbPath = Path.Combine(env.WebRootPath, "GeoLite2-Country.mmdb");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? context.Connection.RemoteIpAddress?.ToString();

        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access Denied: Unable to process IP address.");
            return;
        }

        if (await _blockedIpRepository.IsBlockedAsync(ipAddress))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access Denied: Your IP is blocked.");
            return;
        }

        try
        {
            using (var reader = new DatabaseReader(_geoLite2DbPath))
            {
                var country = reader.Country(ipAddress);

                if (country.Country.IsoCode != "TH")
                {
                    await _blockedIpRepository.AddBlockedIpAsync(ipAddress, "Not from Thailand");

                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access Denied: Only Thailand IPs are allowed.");
                    return;
                }
            }
        }
        catch (AddressNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync($"Access Denied: IP address '{ipAddress}' not found in database.");
            return;
        }

        await _next(context);
    }
}



}