using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class AnswerOption
{
    public int Id { get; set; }

    public int IdTask { get; set; }

    public string Text { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public virtual Task IdTaskNavigation { get; set; } = null!;

    public virtual ICollection<UserAnswerOption> UserAnswerOptions { get; set; } = new List<UserAnswerOption>();
}
