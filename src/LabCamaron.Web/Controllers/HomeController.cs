using Microsoft.AspNetCore.Mvc;

namespace LabCamaron.Web.Controllers;

public class HomeController() : BaseController
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ErrorAutorizacion() => View();

    public IActionResult ErrorMantenimiento() => View();

    public IActionResult ErrorComun() => View();
}