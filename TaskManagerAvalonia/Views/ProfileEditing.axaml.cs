using Avalonia.Controls;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class ProfileEditing : UserControl
{
    public ProfileEditing()
    {
        InitializeComponent();
    }

    public ProfileEditing(int id)
    {
        InitializeComponent();
        DataContext = new AddAndEditUserViewModel(id);
    }
}
