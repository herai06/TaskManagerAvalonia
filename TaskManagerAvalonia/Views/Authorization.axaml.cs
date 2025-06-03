using Avalonia.Controls;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class Authorization : UserControl
{
    public Authorization()
    {
        InitializeComponent();
        DataContext = new AuthorizationViewModel();
    }
}
