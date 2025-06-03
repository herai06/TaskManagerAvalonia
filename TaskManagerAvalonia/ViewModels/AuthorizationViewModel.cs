using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using TaskManagerAvalonia.Models;

namespace TaskManagerAvalonia.ViewModels
{
    internal class AuthorizationViewModel : ViewModelBase
    {
        string _login = "eksh@gmail.com";
        string _password = "123";

        public string Login
        {
            get => _login;
            set => _login = value;
        }
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public async void AuthUser()
        {
            byte[] hashPassword = MD5.HashData(Encoding.ASCII.GetBytes(_password));
            User? user = MainWindowViewModel
                .myConnection.Users.Include(x => x.IdRoleNavigation)
                .FirstOrDefault(x => x.Login == _login && x.Password == hashPassword);
            if (user != null)
            {
                MainWindowViewModel.Instance.UC = new HomeView(user);
            }
            else if (String.IsNullOrWhiteSpace(Password) || String.IsNullOrWhiteSpace(Login))
            {
                ButtonResult result = await MessageBoxManager
                    .GetMessageBoxStandard(
                        "Сообщение",
                        "Заполните все поля для авторизации!",
                        ButtonEnum.Ok
                    )
                    .ShowAsync();
            }
            else
            {
                ButtonResult result = await MessageBoxManager
                    .GetMessageBoxStandard("Сообщение", "Пользователь не найден!", ButtonEnum.Ok)
                    .ShowAsync();
            }
        }
    }
}
