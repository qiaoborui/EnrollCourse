using System;
using System.Collections.Generic;

namespace EnrollCourse.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public int? Credit { get; set; }

    public virtual ICollection<Selectedcourse> Selectedcourses { get; set; } = new List<Selectedcourse>();
}
