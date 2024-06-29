using EnrollCourse.Models;

namespace EnrollCourse.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnrollCourse.Models;

public class StudentController: Controller
{
    private readonly ILogger<StudentController> _logger;
    
    public StudentController(ILogger<StudentController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult StudentEdit(int id)
    {
        string idnum = id.ToString();
        if (string.IsNullOrEmpty(idnum))
        {
            return View();
        }

        //查询学生列表
        using (var db = new _2109060226DbContext())
        {
            var student = db.Students.Find(id);
            //返回学生列表给视图，以便使用强类型展示
            return View(student);
        }
    }
    
    public IActionResult StudentDelete(int id)
    {
        Console.WriteLine("StudentDelete id: " + id);
        // delete student
        
        using (var db = new _2109060226DbContext())
        {
            var student = db.Students.Find(id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            } 
            else 
            {
                Console.WriteLine("Student not found");
                return RedirectToAction("ShowAllStudent", "Student");
            }
        }
        return RedirectToAction("ShowAllStudent", "Student");
    }
    
    public IActionResult SaveStudent(Student student)
    {
        using (var db = new _2109060226DbContext())
        {
            _logger.LogInformation("SaveStudent");
            var model = db.Students.Find(student.StudentId);
            if (model == null)
            {
                model = new Student();
                model.StudentId = student.StudentId;
                model.StudentName = student.StudentName;
                model.Class = student.Class;
                model.InitialPassword = student.InitialPassword;
                db.Students.Add(model);
                _logger.LogInformation("SaveStudent Add");
            }
            else
            {
                model.StudentName = student.StudentName;
                model.Class = student.Class;
                model.InitialPassword = student.InitialPassword;
                _logger.LogInformation("SaveStudent Update");
            }

            db.SaveChanges();
        }
        return RedirectToAction("ShowAllStudent", "Student");
    }
    
    public IActionResult ShowAllStudent()
    {
        using (var db = new _2109060226DbContext())
        {
            var students = db.Students.ToList();
            return View(students);
        }
    }
}