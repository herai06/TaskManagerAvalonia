using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class UserAnswer
{
    public int Id { get; set; }

    public int IdTask { get; set; }

    public int IdStatistics { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime AnswerTime { get; set; }

    public bool IsCorrect { get; set; }

    public virtual Statistic IdStatisticsNavigation { get; set; } = null!;

    public virtual Task IdTaskNavigation { get; set; } = null!;

    public virtual ICollection<UserAnswerOption> UserAnswerOptions { get; set; } = new List<UserAnswerOption>();
}
