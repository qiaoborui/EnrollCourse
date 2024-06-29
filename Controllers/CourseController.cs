using EnrollCourse.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnrollCourse.Controllers;

public class CourseController : MyBaseController
{
    public CourseController()
    {
        
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }
    

    public IActionResult CourseEdit(int id)
    {
        string idnum = id.ToString();
        if (string.IsNullOrEmpty(idnum))
        {
            return View();
        }

        //查询课程列表
        using (var db = new _2109060226DbContext())
        {
            var course = db.Courses.Find(id);
            //返回课程列表给视图，以便使用强类型展示
            return View(course);
        }
    }

    public IActionResult CourseDelete(int id)
    {
        Console.WriteLine("CourseDelete id: " + id);
        // delete course
        
        using (var db = new _2109060226DbContext())
        {
            var course = db.Courses.Find(id);
            if (course != null)
            {
                // 判断是否有人选课
                var selectedCourse = db.Selectedcourses.FirstOrDefault(sc => sc.CourseId == id);
                if (selectedCourse != null)
                {
                    Console.WriteLine("Course has been selected by student, cannot delete");
                    return RedirectToAction("Error", "Home", new {reason="Course has been selected by student, cannot delete"});
                }
                db.Courses.Remove(course);
                db.SaveChanges();
            } 
            else 
            {
                Console.WriteLine("Course not found");
                return RedirectToAction("ShowAllCourse", "Course");
            }
        }
        return RedirectToAction("ShowAllCourse", "Course");
    }
    public IActionResult SaveCourse(Course course)
    {
        using (var db = new _2109060226DbContext())
        {
            var model = db.Courses.Find(course.CourseId);
       
            if (model == null)
            {
                model = new Course();
                model.CourseId = course.CourseId;
                model.CourseName = course.CourseName;
                model.Credit = course.Credit;
                db.Courses.Add(model);
            }
            else
            {
                model.CourseName = course.CourseName;
                model.Credit = course.Credit;
            }
            db.SaveChanges();
        }
        return RedirectToAction("ShowAllCourse", "Course");
    }
    
    public IActionResult ShowAllCourse()
    {
        //将用户名存入viewdata,以便在视图中显示
        ViewData["Username"] = HttpContext.Session.GetString("Username");
        //查询课程列表
        using (var db = new _2109060226DbContext())
        {
            List<Course> courses = db.Courses.ToList();
            //返回课程列表给视图，以便使用强类型展示
            return View(courses);
        }
    }
}