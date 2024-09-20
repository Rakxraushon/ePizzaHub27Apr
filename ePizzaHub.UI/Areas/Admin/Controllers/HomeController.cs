using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            string name = CurrentUser.Name;
            return View();
        }
    }
}
