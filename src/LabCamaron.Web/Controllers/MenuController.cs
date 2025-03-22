using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers
{
    public class MenuController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}