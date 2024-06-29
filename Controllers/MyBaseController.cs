using Microsoft.AspNetCore.Mvc;

namespace EnrollCourse.Controllers;

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