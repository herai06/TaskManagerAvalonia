using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using TaskManagerAvalonia.Models;

namespace TaskManagerAvalonia.ViewModels
{
    internal class AllTeachersCoursesViewModel : ReactiveObject
    {
        private readonly int _teacherId;
        User _currentTeacher;
        List<Course> _courses;

        public User CurrentTeacher
        {
            get => _currentTeacher;
            set => this.RaiseAndSetIfChanged(ref _currentTeacher, value);
        }
        public List<Course> Courses
        {
            get => _courses;
            set => this.RaiseAndSetIfChanged(ref _courses, value);
        }

        public AllTeachersCoursesViewModel(int idTeacher)
        {
            _teacherId = idTeacher;
            CurrentTeacher = MainWindowViewModel.myConnection.Users.First(x => x.Id == idTeacher);
            Courses = MainWindowViewModel
                .myConnection.Courses.Where(x => x.IdTeacherNavigation.Id == idTeacher)
                .Include(x => x.Tests)
                .ToList();

            AddCourseCommand = ReactiveCommand.Create(AddCourse);
            EditCourseCommand = ReactiveCommand.Create<int>(EditCourse);
        }

        public ReactiveCommand<Unit, Unit> AddCourseCommand { get; }
        public ReactiveCommand<int, Unit> EditCourseCommand { get; }

        private void AddCourse()
        {
            if (
                MainWindowViewModel.Instance.UC is HomeView homeView
                && homeView.DataContext is HomeViewModel homeViewModel
            )
            {
                homeViewModel.CurrentPage = new AddAndEditCourse(_teacherId);
            }
        }

        private void EditCourse(int courseId)
        {
            if (
                MainWindowViewModel.Instance.UC is HomeView homeView
                && homeView.DataContext is HomeViewModel homeViewModel
            )
            {
                homeViewModel.CurrentPage = new AddAndEditCourse(_teacherId, courseId);
            }
        }

        public async void DeleteCourse(int idCourse)
        {
            ButtonResult result = await MessageBoxManager
                .GetMessageBoxStandard(
                    "Сообщение",
                    "Вы действительно хотите удалить всю информацию о курсе?",
                    ButtonEnum.YesNo
                )
                .ShowAsync();
            switch (result)
            {
                case ButtonResult.Yes:
                {
                    Course deletedCourse = MainWindowViewModel.myConnection.Courses.First(x =>
                        x.Id == idCourse
                    );
                    MainWindowViewModel.myConnection.Courses.Remove(deletedCourse);
                    MainWindowViewModel.myConnection.SaveChanges();
                    MainWindowViewModel.Instance.UC = new HomeView(CurrentTeacher);
                    break;
                }
                case ButtonResult.No:
                {
                    break;
                }
            }
        }
    }
}
