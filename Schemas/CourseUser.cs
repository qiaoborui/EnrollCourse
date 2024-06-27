namespace WebApplication2.Schemas;

public class CourseUser
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public int? Credit { get; set; }

    public bool IsSelect { get; set; }
}
