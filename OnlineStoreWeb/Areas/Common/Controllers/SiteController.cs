namespace OnlineStoreWeb.Areas.Common.Controllers;

using Microsoft.AspNetCore.Mvc;

[Area("common")]
public class SiteController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("privacy")]
    public IActionResult Privacy()
    {
        return View();
    }
}
