using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public class MyBaseController : Controller
{
    public  MyBaseController()
    {
        
    }

    // GET
    public IActionResult Index()
    {
        
        return View();
    }
}