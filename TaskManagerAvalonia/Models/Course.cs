using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int IdTeacher { get; set; }

    public virtual User IdTeacherNavigation { get; set; } = null!;

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
