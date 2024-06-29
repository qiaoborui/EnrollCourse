using EnrollCourse.Models;
using EnrollCourse.Schemas;

namespace EnrollCourse.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnrollCourse.Models;
using EnrollCourse.Schemas;

public class EnrolledCourseController: Controller
{
    private readonly ILogger<StudentController> _logger;
    
    public EnrolledCourseController(ILogger<StudentController> logger)
    {
        _logger = logger;
    }
    
    
    public IActionResult ShowAllEnrolledCourse()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");
        //查询课程列表
        using (var db = new _2109060226DbContext())
        {
            var username = HttpContext.Session.GetString("Username");
            var courses = db.Courses.ToList();
            //返回课程列表给视图，以便使用强类型展示
            var selectedCourses = db.Selectedcourses.Where(c => c.StudentId == int.Parse(username)).ToList();
            // convert model.course to courseuser
            var courseUserList = new List<CourseUser>();
            foreach (var course in courses)
            {
                var courseUser = new CourseUser
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    Credit = course.Credit,
                    IsSelect = selectedCourses.Any(sc => sc.CourseId == course.CourseId)
                };

                courseUserList.Add(courseUser);
            }
            return View(courseUserList);
        }
    }
    
    public IActionResult EnrollCourse(int id)
    {
        var username = HttpContext.Session.GetString("Username");
        
        using (var db = new _2109060226DbContext())
        {
            var student = db.Students.FirstOrDefault(s => s.StudentId == int.Parse(username));
            var course = db.Courses.FirstOrDefault(c => c.CourseId == id);
            _logger.LogInformation("Enroll course {0} for student {1}", id, username);
            if (student != null && course != null)
            {
                _logger.LogInformation("Student {0} and course {1} found", student.StudentId, course.CourseId);
                var selectedCourse = new Selectedcourse
                {
                    CourseId = id,
                    StudentId = student.StudentId,
                    CreateDate = DateTime.Now
                };

                db.Selectedcourses.Add(selectedCourse);
                db.SaveChanges();
                _logger.LogInformation("Course {0} enrolled for student {1}", id, username);
            }
            else
            {
                _logger.LogInformation("Student {0} or course {1} not found", student.StudentId, course.CourseId);
            }
        }
        return RedirectToAction("ShowAllEnrolledCourse");
    }

    public IActionResult SelectedCourse()
    {
        // 返回已选课程列表
        using (var db = new _2109060226DbContext())
        {
            var username = HttpContext.Session.GetString("Username");
            var selectedCourses = db.Selectedcourses.Where(c => c.StudentId == int.Parse(username)).ToList();
            var courses = db.Courses.ToList();
            var courseUserList = new List<CourseUser>();
            foreach (var selectedCourse in selectedCourses)
            {
                var course = courses.FirstOrDefault(c => c.CourseId == selectedCourse.CourseId);
                if (course != null)
                {
                    var courseUser = new CourseUser
                    {
                        CourseId = course.CourseId,
                        CourseName = course.CourseName,
                        Credit = course.Credit,
                        IsSelect = true
                    };
                    courseUserList.Add(courseUser);
                }
            }
            return View(courseUserList);
        }
    }
    
    public IActionResult DropCourse(int id)
    {
        var username = HttpContext.Session.GetString("Username");
        using (var db = new _2109060226DbContext())
        {
            var selectedCourse = db.Selectedcourses.FirstOrDefault(sc => sc.CourseId == id && sc.StudentId == int.Parse(username));
            if (selectedCourse != null)
            {
                db.Selectedcourses.Remove(selectedCourse);
                db.SaveChanges();
            }
        }
        return RedirectToAction("SelectedCourse");
    }
}