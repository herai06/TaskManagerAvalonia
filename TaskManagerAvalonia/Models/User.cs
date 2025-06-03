using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string? Phone { get; set; }

    public DateTime? BirthDate { get; set; }

    public int IdRole { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual UserRole IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Statistic> Statistics { get; set; } = new List<Statistic>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
