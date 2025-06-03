using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class Task
{
    public int Id { get; set; }

    public int IdTest { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[]? Media { get; set; }

    public virtual ICollection<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();

    public virtual Test IdTestNavigation { get; set; } = null!;

    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
