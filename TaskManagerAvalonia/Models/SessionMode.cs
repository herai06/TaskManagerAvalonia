using System.Collections.Generic;

namespace TaskManagerAvalonia.Models;

public partial class SessionMode
{
    public int Id { get; set; }

    public string Mode { get; set; } = null!;

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
