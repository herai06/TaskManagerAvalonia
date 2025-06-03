using Avalonia.Controls;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class AllTeachersCourses : UserControl
{
    public AllTeachersCourses(int id)
    {
        InitializeComponent();
        DataContext = new AllTeachersCoursesViewModel(id);
    }
}
