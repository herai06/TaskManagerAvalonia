using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using TaskManagerAvalonia.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagerAvalonia.ViewModels
{
    internal class AddAndEditUserViewModel : ViewModelBase
    {
        User _currentUser = new User();
        string _enteredPassword;
        public User CurrentUser
        {
            get => _currentUser;
            set => this.RaiseAndSetIfChanged(ref _currentUser, value);
        }
        public string EnteredPassword
        {
            get => _enteredPassword;
            set => this.RaiseAndSetIfChanged(ref _enteredPassword, value);
        }

        public AddAndEditUserViewModel()
        {
            _currentUser = new User { IdRoleNavigation = new UserRole() };
        }

        public AddAndEditUserViewModel(int id)
        {
            _currentUser = MainWindowViewModel.myConnection.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<UserRole> ListUserRole =>
            MainWindowViewModel.myConnection.UserRoles.Where(x => x.Id != 3).ToList();

        public async void AddNewUser()
        {
            if (CurrentUser.Id == 0)
            {
                CurrentUser.Password = MD5.HashData(Encoding.ASCII.GetBytes(EnteredPassword));
                MainWindowViewModel.myConnection.Users.Add(CurrentUser);
                MainWindowViewModel.myConnection.SaveChanges();
                CurrentUser = new User();
                EnteredPassword = string.Empty;
                ButtonResult result = await MessageBoxManager
                    .GetMessageBoxStandard(
                        "Сообщение",
                        "Пользователь успешно зарегистрирован!",
                        ButtonEnum.Ok
                    )
                    .ShowAsync();
            }
            else
            {
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.UC = new HomeView(CurrentUser);
            }
        }

        public async Task EditPhoto()
        {
            if (
                Application.Current?.ApplicationLifetime
                    is not IClassicDesktopStyleApplicationLifetime desctop
                || desctop.MainWindow?.StorageProvider is not { } provider
            )
                throw new NullReferenceException("провайдер отсутствует");
            var file = await provider.OpenFilePickerAsync(
                new FilePickerOpenOptions()
                {
                    Title = "Выберте изображение",
                    AllowMultiple = false,
                    FileTypeFilter = [FilePickerFileTypes.ImageAll],
                }
            );
            if (file != null)
            {
                await using var readStream = await file[0].OpenReadAsync();
                byte[] buffer = new byte[readStream.Length];
                readStream.ReadAtLeast(buffer, 1);
                CurrentUser.Image = buffer;
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.UC = new ProfileEditing(CurrentUser.Id);
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
    }
}
