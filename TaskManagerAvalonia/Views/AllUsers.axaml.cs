using Avalonia.Controls;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class AllUsers : UserControl
{
    public AllUsers(int idCurrentUser)
    {
        InitializeComponent();
        DataContext = new AllUsersViewModel(idCurrentUser);
    }
}
