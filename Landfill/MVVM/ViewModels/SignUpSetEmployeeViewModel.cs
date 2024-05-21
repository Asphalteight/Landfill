﻿using Landfill.Abstractions;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class SignUpSetEmployeeViewModel : ViewModelBase
    {
        #region Проперти

        private readonly IDbContext _dbContext;
        private readonly ICredentialsService _credentialsService;
        private ErrorMessageModel _errorMessage;
        private EmployeeInfoModel _employeeInfo = new();
        private INavigationService _navigation;

        public ErrorMessageModel ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(); } }
        public EmployeeInfoModel EmployeeInfo { get => _employeeInfo; set { _employeeInfo = value; OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public ICommand PreviousStepCommand { get; }
        public ICommand SignUpCommand { get; }

        #endregion

        public SignUpSetEmployeeViewModel(INavigationService navigation, IDbContext dbContext, ICredentialsService userInfoService)
        {
            Navigation = navigation;
            _dbContext = dbContext;
            _credentialsService = userInfoService;

            PreviousStepCommand = new ViewModelCommand(x => Navigation.NavigateTo<SignUpViewModel>());
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            var canExecute = !EmployeeInfo.FirstName.IsNullOrWhiteSpace();

            return canExecute;
        }

        private void ExecuteSignUpCommand(object obj)
        {
            var credentials = _credentialsService.Get();
            var salt = PasswordHelper.GenerateSalt();
            var passwordHash = credentials.Password.GetHash(salt);

            var user = new UserAccount()
            {
                Login = credentials.Login,
                PasswordHash = passwordHash,
                Salt = salt
            };
            var employee = new Employee 
            {
                FirstName = EmployeeInfo.FirstName,
                LastName = EmployeeInfo.LastName,
                MiddleName = EmployeeInfo.MiddleName,
                Phone = EmployeeInfo.Phone
            };
            user.Employee = employee;
            user.Roles.Add(new RoleToUser { Role = DataAccess.Models.Enums.RoleEnum.User });

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            RunCommand(x => Navigation.NavigateTo<SignUpSuccessViewModel>());
        }
    }
}
