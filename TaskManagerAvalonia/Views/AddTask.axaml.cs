using System;
using Avalonia.Controls;
using TaskManagerAvalonia.Models;
using TaskManagerAvalonia.ViewModels;

namespace TaskManagerAvalonia;

public partial class AddTask : UserControl
{
    private readonly AddTestViewModel _parentViewModel;

    public AddTask(AddTestViewModel parentViewModel)
    {
        InitializeComponent();
        _parentViewModel = parentViewModel;
        DataContext = new AddTaskViewModel(parentViewModel);
    }
}
