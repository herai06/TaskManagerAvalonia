using Avalonia.Controls;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class AddAndEditCourse : UserControl
{
    public AddAndEditCourse(int idTeacher)
    {
        InitializeComponent();
        DataContext = new AddAndEditCourseViewModel(idTeacher);
    }

    public AddAndEditCourse(int idTeacher, int idCourse)
    {
        InitializeComponent();
        DataContext = new AddAndEditCourseViewModel(idTeacher, idCourse);
    }
}
