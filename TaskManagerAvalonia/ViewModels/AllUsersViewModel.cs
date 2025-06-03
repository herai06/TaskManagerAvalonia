using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using TaskManagerAvalonia.Models;

namespace TaskManagerAvalonia.ViewModels
{
    internal class AllUsersViewModel : ViewModelBase
    {
        List<User> _listUser;
        User _currentUser;

        public List<User> ListUser
        {
            get => _listUser;
            set => this.RaiseAndSetIfChanged(ref _listUser, value);
        }
        public User CurrentUser
        {
            get => _currentUser;
            set => this.RaiseAndSetIfChanged(ref _currentUser, value);
        }

        public AllUsersViewModel(int idCurrentUser)
        {
            ListUser = MainWindowViewModel
                .myConnection.Users.Include(x => x.IdRoleNavigation)
                .Where(x => x.IdRoleNavigation.Id != 3)
                .Include(x => x.Courses)
                .Include(x => x.StudentCourses)
                .ToList();
            CurrentUser = MainWindowViewModel.myConnection.Users.First(x => x.Id == idCurrentUser);
        }

        List<UserRole> _userRole =
        [
            new UserRole() { Id = 0, Role = "Все пользователи" },
            .. MainWindowViewModel.myConnection.UserRoles.Where(x => x.Id != 3).ToList(),
        ];

        public List<UserRole> AllRole
        {
            get => _userRole;
        }

        UserRole _selectedRole = null;
        public UserRole SelectedRole
        {
            get
            {
                if (_selectedRole == null)
                    return _userRole[0];
                else
                    return _selectedRole;
            }
            set
            {
                _selectedRole = value;
                Filters();
            }
        }

        void Filters()
        {
            ListUser = MainWindowViewModel
                .myConnection.Users.Include(x => x.IdRoleNavigation)
                .Where(x => x.IdRoleNavigation.Id != 3)
                .Include(x => x.Courses)
                .Include(x => x.StudentCourses)
                .ToList();

            if (_selectedRole != null && _selectedRole.Id != 0)
            {
                ListUser = ListUser.Where(x => x.IdRoleNavigation.Id == _selectedRole.Id).ToList();
            }
        }

        public async void DeleteUser(int idDeleteUser)
        {
            ButtonResult result = await MessageBoxManager
                .GetMessageBoxStandard(
                    "Сообщение",
                    "При удалении пользователя также будет удалена информация о курсах и тестах.\n"
                        + "Вы действительно хотите удалить пользователя?",
                    ButtonEnum.YesNo
                )
                .ShowAsync();
            switch (result)
            {
                case ButtonResult.Yes:
                {
                    User deleteUser = MainWindowViewModel.myConnection.Users.First(x =>
                        x.Id == idDeleteUser
                    );
                    MainWindowViewModel.myConnection.Users.Remove(deleteUser);
                    MainWindowViewModel.myConnection.SaveChanges();
                    MainWindowViewModel.Instance.UC = new HomeView(CurrentUser);
                    await MessageBoxManager
                        .GetMessageBoxStandard(
                            "Сообщение",
                            "Пользователь успешно удалён из базы!",
                            ButtonEnum.Ok
                        )
                        .ShowAsync();
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
