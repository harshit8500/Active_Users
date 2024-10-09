using Microsoft.AspNetCore.Mvc;

// Controllers/AccountController.cs
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
//using UserActivityTracking.Data;
//using UserActivityTracking.Models;
using SampleActiveUsers.NewtrsyModel;
using SampleActiveUsers.Data;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly SmplActurContext _context;

    public AccountController(SmplActurContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Login()
    {
        int userId = 1;  // Replace this with actual userId after authentication
        string userAgent = Request.Headers["User-Agent"].ToString();
        string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        var userActivity = new UserActivity
        {
            UserId = userId,
            LoginTime = DateTime.UtcNow,
            UserAgent = userAgent,
            IpAddress = ipAddress
        };

        _context.UserActivities.Add(userActivity);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("UserActivityId", userActivity.Id);

        return RedirectToAction("Dashboard");
    }

    public IActionResult Logout()
    {
        int? activityId = HttpContext.Session.GetInt32("UserActivityId");

        if (activityId != null)
        {
            var userActivity = _context.UserActivities.FirstOrDefault(x => x.Id == activityId.Value);

            if (userActivity != null)
            {
                userActivity.LogoutTime = DateTime.UtcNow;
                userActivity.SessionDuration = (int)(userActivity.LogoutTime - userActivity.LoginTime)?.TotalSeconds;
                _context.SaveChanges();
            }
        }

        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }

    public IActionResult Dashboard()
    {
        return View();
    }
}


























/*
namespace SampleActiveUsers.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

// Controllers/AccountController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using SampleActiveUsers.Data;
//using YourNamespace.Data;  // Replace with the actual namespace for SmplActurContext
//using YourNamespace.NewtrsyModel;  // Replace with the actual namespace for models
namespace SampleActiveUsers.NewtrsyModel;

namespace SampleActiveUsers.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly SmplActurContext _context;

        public AccountController(SmplActurContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login()
        {
            int userId = 1;  // Replace this with actual userId after authentication
            string userAgent = Request.Headers["User-Agent"].ToString();
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var userActivity = new UserActivity // Ensure this is in the SmplActurContext models
            {
                UserId = userId,
                LoginTime = DateTime.UtcNow,
                UserAgent = userAgent,
                IpAddress = ipAddress
            };

            _context.UserActivities.Add(userActivity);
            _context.SaveChanges();

            // Store activity in session for tracking during logout
            HttpContext.Session.SetInt32("UserActivityId", userActivity.Id);

            return RedirectToAction("Dashboard");
        }

        public IActionResult Logout()
        {
            int? activityId = HttpContext.Session.GetInt32("UserActivityId");

            if (activityId != null)
            {
                var userActivity = _context.UserActivities.FirstOrDefault(x => x.Id == activityId.Value);

                if (userActivity != null)
                {
                    // Set the logout time and session duration when the user logs out
                    userActivity.LogoutTime = DateTime.UtcNow;
                    userActivity.SessionDuration = (int)(userActivity.LogoutTime - userActivity.LoginTime)?.TotalSeconds;
                    _context.SaveChanges();
                }
            }

            // Clear session upon logout
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }*/


