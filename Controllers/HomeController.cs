using GymPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymPlanner.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // User is logged in, get their username
                SetCookies("userName"
               , User.Identity.Name);
            }
            else
            {
                // User is not logged in, set a cookie with "Guest"
                SetCookies("userName"
               , "guest");
            }
            SetCookies("broswerName"
           , Request.Headers["User-Agent"].ToString());
            return View();

        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetCookies(string cookieName, string cookieValue)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(15), // Cookie expires in 14 days
                HttpOnly = true, // Prevent JavaScript access to the cookies
                Secure = true, // Use Secure flage
                SameSite = SameSiteMode.Strict // Prevent CSRF attacks
            };
            Response.Cookies.Append(cookieName, cookieValue, options);
            return Ok("Cookies has been set.");
        }
    }
}
