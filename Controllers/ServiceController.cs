using Microsoft.AspNetCore.Mvc;

namespace RH.Controllers{
    public class ServiceController : Controller{
    public IActionResult Liste()
    {
        return View("Views/Home/liste_service.cshtml");
    }
}
}