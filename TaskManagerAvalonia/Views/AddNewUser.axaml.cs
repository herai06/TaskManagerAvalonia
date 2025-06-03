using Avalonia.Controls;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class AddNewUser : UserControl
{
    public AddNewUser()
    {
        InitializeComponent();
        DataContext = new AddAndEditUserViewModel();
    }

    public AddNewUser(int id)
    {
        InitializeComponent();
        DataContext = new AddAndEditUserViewModel(id);
    }
}
