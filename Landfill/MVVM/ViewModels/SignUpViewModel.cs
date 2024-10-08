﻿using Landfill.Abstractions;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.Common.Helpers;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    /// <summary>
    /// Регистрация 1-й этап
    /// </summary>
    public partial class SignUpViewModel : ViewModelBase
    {
        #region Поля и свойства

        private readonly IDbContext _dbContext;
        private readonly ICredentialsService _credentialsService;
        private ErrorMessageModel _errorMessage;
        private INavigationService _navigation;

        public CredentialsModel Credentials { get; set; } = new();
        public ErrorMessageModel ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public ICommand BackToSignInCommand { get; }
        public ICommand NextStepCommand { get; }

        #endregion

        public SignUpViewModel(INavigationService navigation, IDbContext dbContext, ICredentialsService userInfoService)
        {
            Navigation = navigation;
            _dbContext = dbContext;
            _credentialsService = userInfoService;

            BackToSignInCommand = new ViewModelCommand(x => Navigation.NavigateTo<SignInViewModel>());
            NextStepCommand = new ViewModelCommand(ExecuteNextStepCommand, CanExecuteNextStepCommand);
            Credentials = _credentialsService.Get();
        }

        private bool CanExecuteNextStepCommand(object obj)
        {
            var canExecute = true;
            if ((new[] { Credentials.Login, Credentials.Password, Credentials.PasswordDublicate }).Any(x => x.IsNullOrWhiteSpace()))
            {
                canExecute = false;
            }
            return canExecute;
        }

        private void ExecuteNextStepCommand(object obj)
        {
            var user = _dbContext.QuerySet<UserAccount>().FirstOrDefault(x => x.Login == Credentials.Login);
            if (user != null)
            {
                ErrorMessage = new() { Login = "*Пользователь с таким именем уже существует" };
                return;
            }

            if (Credentials.Password != Credentials.PasswordDublicate)
            {
                ErrorMessage = new() { PasswordDublicate = "*Пароли не совпадают" };
                return;
            }

            var numbersRegex = MyRegex();
            if (!numbersRegex.IsMatch(Credentials.Password))
            {
                ErrorMessage = new() { Password = "*Пароль должен содержать хотя бы одну цифру" };
                return;
            }

            _credentialsService.Set(Credentials);

            RunCommand(x => Navigation.NavigateTo<SignUpSetEmployeeViewModel>());
        }

        [GeneratedRegex(@"[0-9]+")]
        private static partial Regex MyRegex();
    }
}
