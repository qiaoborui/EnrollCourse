using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Selectedcourse
{
    public DateTime? CreateDate { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
