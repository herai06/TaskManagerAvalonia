namespace TaskManagerAvalonia.Models;

public partial class Course
{
    public string? AvailabilityDescription
    {
        get
        {
            if (string.IsNullOrEmpty(Description))
                return " —";
            else
                return Description;
        }
    }

    public bool AvailabilityTests
    {
        get
        {
            if (Tests.Count == 0)
                return false;
            else
                return true;
        }
    }
}
