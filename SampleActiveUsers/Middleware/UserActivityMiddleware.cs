using Microsoft.AspNetCore.Http;
using SampleActiveUsers.Data;
using SampleActiveUsers.NewtrsyModel;
using System.Threading.Tasks;
using System;

namespace SampleActiveUsers.Middleware
{
    public class UserActivityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SmplActurContext _context;

        public UserActivityMiddleware(RequestDelegate next, SmplActurContext context)
        {
            _next = next;
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(context.User.Identity.Name); // Assuming the user's ID is in the Name claim
                var ipAddress = context.Connection.RemoteIpAddress?.ToString();
                var userAgent = context.Request.Headers["User-Agent"].ToString();
                var loginTime = DateTime.UtcNow;

                var activity = new UserActivity
                {
                    UserId = userId,
                    LoginTime = loginTime,
                    UserAgent = userAgent,
                    IpAddress = ipAddress
                };

                _context.UserActivities.Add(activity);
                await _context.SaveChangesAsync();
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
