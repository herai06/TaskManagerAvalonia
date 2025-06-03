using Avalonia.Controls;
using Avalonia.Interactivity;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class AddTest : UserControl
{
    //public AddTest(int idTeacher)
    //{
    //    InitializeComponent();
    //    DataContext = new AddTestViewModel(idTeacher);
    //}

    private AddTestViewModel _viewModel;

    public AddTest(int userId)
    {
        InitializeComponent();
        _viewModel = new AddTestViewModel(userId);
        DataContext = _viewModel;
    }

    public AddTestViewModel GetViewModel() => _viewModel;

    public void RestoreViewModel(AddTestViewModel vm)
    {
        _viewModel = vm;
        DataContext = _viewModel;
    }

    private void OnTextBoxLostFocus(object? sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "Новый тест";
        }
    }
}
