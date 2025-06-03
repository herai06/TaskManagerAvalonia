using Avalonia.Controls;
using TaskManagerAvalonia.Models;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class HomeView : UserControl
{
    public HomeView(User currentUser)
    {
        InitializeComponent();
        DataContext = new HomeViewModel(currentUser);
    }

    public AddTest GetCachedAddTest()
    {
        if (DataContext is HomeViewModel homeViewModel)
        {
            return homeViewModel.GetCachedAddTest();
        }
        return null;
    }
}
