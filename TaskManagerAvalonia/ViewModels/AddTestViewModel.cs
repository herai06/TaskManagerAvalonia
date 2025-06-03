using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using TaskManagerAvalonia.Models;
using TaskTreeManagementSystem.Logic;

namespace TaskManagerAvalonia.ViewModels
{
    public class AddTestViewModel : ViewModelBase
    {
        //private readonly ITaskManager _taskManager;
        //User? _currentUser;
        //Test? _newTest = new Test();

        //public Test? NewTest
        //{
        //    get => _newTest;
        //    set => this.RaiseAndSetIfChanged(ref _newTest, value);
        //}

        //public ReactiveCommand<Unit, Unit> AddTaskCommand { get; }

        //public ObservableCollection<Task> Tasks { get; } = new();

        //public AddTestViewModel(int idTeacher)
        //{
        //    AddTaskCommand = ReactiveCommand.Create(AddTask);
        //    _currentUser = MainWindowViewModel.myConnection.Users.First(x => x.Id == idTeacher);
        //    _newTest.Name = "Новый тест";
        //}

        //private void AddTask()
        //{
        //    if (
        //        MainWindowViewModel.Instance.UC is HomeView homeView
        //        && homeView.DataContext is HomeViewModel homeViewModel
        //    )
        //    {
        //        homeViewModel.CurrentPage = new AddTask(task =>
        //        {
        //            Tasks.Add(task);
        //        });
        //    }
        //}

        private readonly int _userId;
        private ObservableCollection<Task> _tasks = new();
        private string _testTitle = "Новый тест";
        private string _testDescription;

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set => this.RaiseAndSetIfChanged(ref _tasks, value);
        }

        public string TestTitle
        {
            get => _testTitle;
            set => this.RaiseAndSetIfChanged(ref _testTitle, value);
        }

        public string TestDescription
        {
            get => _testDescription;
            set => this.RaiseAndSetIfChanged(ref _testDescription, value);
        }

        public ReactiveCommand<Unit, Unit> AddTaskCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveTestCommand { get; }

        public int UserId => _userId;

        public AddTestViewModel(int userId)
        {
            _userId = userId;
            AddTaskCommand = ReactiveCommand.Create(ExecuteAddTask);
            SaveTestCommand = ReactiveCommand.Create(ExecuteSaveTest);
        }

        private void ExecuteAddTask()
        {
            if (
                MainWindowViewModel.Instance.UC is HomeView homeView
                && homeView.DataContext is HomeViewModel homeViewModel
            )
            {
                homeViewModel.CurrentPage = new AddTask(this);
            }
        }

        private void ExecuteSaveTest()
        {
            // Реализация сохранения теста в БД
            // using var db = new AppDbContext();
            // db.Tests.Add(new Test { ... });
            // db.SaveChanges();
        }

        public void AddNewTask(Task task)
        {
            Tasks.Add(task);
        }
    }
}
