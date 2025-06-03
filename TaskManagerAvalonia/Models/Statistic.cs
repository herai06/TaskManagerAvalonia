using System;
using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class Statistic
{
    public int Id { get; set; }

    public int CorrectAnswersCount { get; set; }

    public int IncorrectAnswersCount { get; set; }

    public double SuccessRate { get; set; }

    public DateTime TotalTime { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
