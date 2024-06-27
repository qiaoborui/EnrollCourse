using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    public IActionResult ShowLogin()
    {
        return View();
    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult LoginUser(LoginUserModel loginUser)
    {
        if (loginUser.Password == "123456")
        {
            //登录成功，将用户名存入Session
            //导入Cookie
            HttpContext.Session.SetString("Username",loginUser.UserName);
            HttpContext.Session.SetString("UserType",loginUser.UserType);
            
            return RedirectToAction("ShowAllCourse","Course");
        }
        else
        {
            ViewData["Message"] = "Invalid User Name or Password !";
            return View("ShowLogin");
        }
        
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}