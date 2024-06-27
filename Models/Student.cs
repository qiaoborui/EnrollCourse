using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? Class { get; set; }

    public string? InitialPassword { get; set; }

    public virtual ICollection<Selectedcourse> Selectedcourses { get; set; } = new List<Selectedcourse>();
}
