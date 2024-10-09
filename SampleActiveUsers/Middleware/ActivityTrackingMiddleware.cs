// Middleware/ActivityTrackingMiddleware.cs
using Microsoft.AspNetCore.Http;
using SampleActiveUsers.Data;
using SampleActiveUsers.NewtrsyModel;
using System;
using System.Threading.Tasks;


public class ActivityTrackingMiddleware
{
    private readonly RequestDelegate _next;

    public ActivityTrackingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, SmplActurContext dbContext)
    {
        var userAgent = context.Request.Headers["User-Agent"].ToString();
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var userId = 1; // Replace this with actual user ID after authentication

        // Log the activity to the database
        var userActivity = new UserActivity
        {
            UserId = userId,
            LoginTime = DateTime.UtcNow,
            UserAgent = userAgent,
            IpAddress = ipAddress
        };

        dbContext.UserActivities.Add(userActivity);
        await dbContext.SaveChangesAsync();

        // Pass control to the next middleware
        await _next(context);
    }
}
public static class ActivityTrackingMiddlewareExtensions
{
    public static IApplicationBuilder UseActivityTrackingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ActivityTrackingMiddleware>();
    }
}
