using Microsoft.AspNetCore.Mvc;
namespace FavBookWebApp.Controllers
{
    public class AccountController :
   Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
