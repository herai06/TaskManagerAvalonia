using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using TaskManagerAvalonia.Models;
using TaskManagerAvalonia.ViewModels.Items;

namespace TaskManagerAvalonia.ViewModels
{
    internal class HomeViewModel : ViewModelBase
    {
        MenuItemViewModel _selectedMenuItem;
        object _currentPage;
        bool _isLast;
        User _user;
        private AddTest _cachedAddTest;
        private int? _currentTeacherId;

        public AddTest GetCachedAddTest()
        {
            return _cachedAddTest;
        }

        public User User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        public MenuItemViewModel SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMenuItem, value);
                UpdateCurrentPage();
            }
        }

        public object CurrentPage
        {
            get => _currentPage;
            set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }
        public bool IsLast
        {
            get => _isLast;
            set => this.RaiseAndSetIfChanged(ref _isLast, value);
        }

        public HomeViewModel(User currentUser)
        {
            _user = currentUser;
            var allMenuItems = new List<MenuItemViewModel>
            {
                new MenuItemViewModel(
                    "Информация о пользователях",
                    false,
                    new List<string> { "Администратор" }
                ),
                new MenuItemViewModel(
                    "Добавить пользователя",
                    true,
                    new List<string> { "Администратор" }
                ),
                new MenuItemViewModel("Мои курсы", false, new List<string> { "Преподаватель" }),
                new MenuItemViewModel("Создать тест", false, new List<string> { "Преподаватель" }),
                new MenuItemViewModel(
                    "Подписать ученика",
                    false,
                    new List<string> { "Преподаватель" }
                ),
                new MenuItemViewModel("Все тесты", false, new List<string> { "Ученик" }),
                new MenuItemViewModel(
                    "История тестов",
                    false,
                    new List<string> { "Преподаватель", "Ученик" }
                ),
                new MenuItemViewModel(
                    "Статистика",
                    true,
                    new List<string> { "Преподаватель", "Ученик" }
                ),
            };

            MenuItems = new ObservableCollection<MenuItemViewModel>(
                allMenuItems.Where(item => item.AllowedRoles.Contains(_user.IdRoleNavigation.Role))
            );
            SelectedMenuItem = MenuItems.FirstOrDefault();
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        private void UpdateCurrentPage()
        {
            switch (SelectedMenuItem.Title)
            {
                case "Добавить пользователя":
                    CurrentPage = new AddNewUser();
                    break;
                case "Информация о пользователях":
                    CurrentPage = new AllUsers(User.Id);
                    break;
                case "Все тесты":
                    break;
                case "Мои курсы":
                    CurrentPage = new AllTeachersCourses(User.Id);
                    break;
                case "Создать тест":
                    if (_cachedAddTest == null || _currentTeacherId != User.Id)
                    {
                        _currentTeacherId = User.Id;
                        _cachedAddTest = new AddTest(User.Id);
                    }
                    CurrentPage = _cachedAddTest;
                    break;
                case "Подписать ученика":
                    break;
                case "История тестов":
                    break;
                case "Статистика":
                    break;
                default:
                    break;
            }
        }

        public async void LogoutToAccount()
        {
            ButtonResult result = await MessageBoxManager
                .GetMessageBoxStandard(
                    "Сообщение",
                    "Вы действительно хотите выйти из профиля?",
                    ButtonEnum.YesNo
                )
                .ShowAsync();
            switch (result)
            {
                case ButtonResult.Yes:
                {
                    MainWindowViewModel.Instance.UC = new Authorization();
                    break;
                }
                case ButtonResult.No:
                {
                    break;
                }
            }
        }

        public void ToEditingPage()
        {
            MainWindowViewModel.Instance.UC = new ProfileEditing(User.Id);
        }
    }
}
