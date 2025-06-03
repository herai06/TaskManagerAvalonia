using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class Test
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int IdCourse { get; set; }

    public int IdSessionMode { get; set; }

    public virtual Course IdCourseNavigation { get; set; } = null!;

    public virtual SessionMode IdSessionModeNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
