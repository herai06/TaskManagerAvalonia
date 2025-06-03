using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class StudentCourse
{
    public int Id { get; set; }

    public int IdStudent { get; set; }

    public int IdCourse { get; set; }

    public virtual Course IdCourseNavigation { get; set; } = null!;

    public virtual User IdStudentNavigation { get; set; } = null!;
}
