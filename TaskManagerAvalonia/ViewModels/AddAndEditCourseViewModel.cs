using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using TaskManagerAvalonia.Models;

namespace TaskManagerAvalonia.ViewModels
{
    internal class AddAndEditCourseViewModel : ViewModelBase
    {
        Course? _currentCourse = new Course();
        User? _currentUser;
        string _namePage = string.Empty;
        string _nameButton = string.Empty;
        bool _isVisibleTests;
        bool _isVisibleTestsPanel;
        private ObservableCollection<Test> _availableTests = new();

        public Course? CurrentCourse
        {
            get => _currentCourse;
            set => this.RaiseAndSetIfChanged(ref _currentCourse, value);
        }
        public string NamePage
        {
            get => _namePage;
            set => this.RaiseAndSetIfChanged(ref _namePage, value);
        }
        public string NameButton
        {
            get => _nameButton;
            set => this.RaiseAndSetIfChanged(ref _nameButton, value);
        }

        public bool IsVisibleTests
        {
            get => _isVisibleTests;
            set => this.RaiseAndSetIfChanged(ref _isVisibleTests, value);
        }

        public ObservableCollection<Test> AvailableTests
        {
            get => _availableTests;
            set => this.RaiseAndSetIfChanged(ref _availableTests, value);
        }
        public bool IsVisibleTestsPanel
        {
            get => _isVisibleTestsPanel;
            set => this.RaiseAndSetIfChanged(ref _isVisibleTestsPanel, value);
        }

        public AddAndEditCourseViewModel(int idTeacher)
        {
            _currentUser = MainWindowViewModel.myConnection.Users.FirstOrDefault(x =>
                x.Id == idTeacher
            );
            _currentCourse = new Course { IdTeacherNavigation = _currentUser! };
            IsVisibleTestsPanel = false;
            _namePage = "Создание курса";
            NameButton = "Добавить курс";
        }

        public AddAndEditCourseViewModel(int idTeacher, int idCourse)
        {
            _currentCourse = MainWindowViewModel.myConnection.Courses.FirstOrDefault(x =>
                x.Id == idCourse
            );
            _currentUser = MainWindowViewModel.myConnection.Users.FirstOrDefault(x =>
                x.Id == idTeacher
            );
            IsVisibleTestsPanel = true;
            if (_currentCourse!.Tests.Count != 0)
                _isVisibleTests = false;
            else
                _isVisibleTests = true;
            _namePage = "Редактирование курса";
            NameButton = "Сохранить";
            _availableTests = new ObservableCollection<Test>(_currentCourse!.Tests);
        }

        public void BackToPageCourses()
        {
            MainWindowViewModel.Instance.UC = new HomeView(_currentUser!);
        }

        public async void DeleteTest(int idTest)
        {
            ButtonResult result = await MessageBoxManager
                .GetMessageBoxStandard(
                    "Сообщение",
                    "Вы действительно хотите удалить тест?",
                    ButtonEnum.YesNo
                )
                .ShowAsync();
            switch (result)
            {
                case ButtonResult.Yes:
                {
                    Test deletedTest = AvailableTests.First(x => x.Id == idTest);
                    AvailableTests.Remove(deletedTest);
                    break;
                }
                case ButtonResult.No:
                {
                    break;
                }
            }
        }

        public async void SaveCourse()
        {
            if (CurrentCourse?.Id == 0)
            {
                if (!string.IsNullOrEmpty(CurrentCourse.Name))
                {
                    MainWindowViewModel.myConnection.Courses.Add(CurrentCourse);
                    MainWindowViewModel.myConnection.SaveChanges();
                    ButtonResult result = await MessageBoxManager
                        .GetMessageBoxStandard(
                            "Сообщение",
                            $"Курс '{CurrentCourse.Name}' успешно добавлен!",
                            ButtonEnum.Ok
                        )
                        .ShowAsync();
                    CurrentCourse = new Course();
                    BackToPageCourses();
                }
                else
                {
                    ButtonResult result = await MessageBoxManager
                        .GetMessageBoxStandard(
                            "Ошибка",
                            "Заполните поле с названием курса!",
                            ButtonEnum.Ok
                        )
                        .ShowAsync();
                }
            }
            else
            {
                foreach (Test deletedTest in CurrentCourse!.Tests)
                {
                    bool flag = false;
                    foreach (Test test in AvailableTests)
                    {
                        if (deletedTest != test)
                            flag = true;
                    }
                    if (flag)
                    {
                        MainWindowViewModel.myConnection.Tests.Remove(deletedTest);
                    }
                }

                MainWindowViewModel.myConnection.SaveChanges();
                ButtonResult result = await MessageBoxManager
                    .GetMessageBoxStandard(
                        "Сообщение",
                        $"Курс '{CurrentCourse?.Name}' успешно изменён!",
                        ButtonEnum.Ok
                    )
                    .ShowAsync();
                BackToPageCourses();
            }
        }
    }
}
