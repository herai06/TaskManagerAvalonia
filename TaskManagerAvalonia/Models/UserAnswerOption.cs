using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class UserAnswerOption
{
    public int Id { get; set; }

    public int IdUserAnswer { get; set; }

    public int IdAnswerOption { get; set; }

    public virtual AnswerOption IdAnswerOptionNavigation { get; set; } = null!;

    public virtual UserAnswer IdUserAnswerNavigation { get; set; } = null!;
}
