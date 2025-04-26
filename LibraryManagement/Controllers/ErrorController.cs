using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

public class ErrorController : Controller
{
    [Route("Error/404")]
    public ActionResult Error404()
    {
        return View("NotFound");
    }

    [Route("Error/{statusCode}")]
    public ActionResult Error(int statusCode)
    {
        if (statusCode == 404)
        {
            return  RedirectToAction("Error404");
        }
        
        return View("Error");
    }
}