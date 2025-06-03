using System;
using System.Reactive;
using ReactiveUI;
using TaskManagerAvalonia.Models;
using TaskTreeManagementSystem.Logic;

namespace TaskManagerAvalonia.ViewModels
{
    internal class AddTaskViewModel : ViewModelBase
    {
        private readonly AddTestViewModel _parentViewModel;
        private string _title;
        private string _description;

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public AddTaskViewModel(AddTestViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;

            SaveCommand = ReactiveCommand.Create(ExecuteSave);
            CancelCommand = ReactiveCommand.Create(ExecuteCancel);
        }

        private void ExecuteSave()
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                var newTask = new Task { Name = Title, Description = Description };
                _parentViewModel.AddNewTask(newTask);
            }
            ReturnToAddTest();
        }

        private void ExecuteCancel()
        {
            ReturnToAddTest();
        }

        private void ReturnToAddTest()
        {
            if (
                MainWindowViewModel.Instance.UC is HomeView homeView
                && homeView.DataContext is HomeViewModel homeViewModel
            )
            {
                // Возвращаемся к сохраненному экземпляру AddTest
                homeViewModel.CurrentPage = homeViewModel.GetCachedAddTest();
            }
        }
    }
}
