using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Linq;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        #region Проперти

        private readonly IDbContext _dbContext;
        private ErrorMessageModel _errorMessage = new();
        private INavigationService _navigation;

        public ErrorMessageModel ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public CredentialsModel Credentials { get; set; } = new();
        public ICommand LoginCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }

        #endregion

        public SignInViewModel(INavigationService navigation, IDbContext dbContext)
        {
            Navigation = navigation;
            _dbContext = dbContext;

            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            NavigateToSignUpCommand = new ViewModelCommand(x => Navigation.NavigateTo<SignUpViewModel>());
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            var isAuthorized = true;
            if (Credentials.Login.IsNullOrWhiteSpace() || Credentials.Password.IsNullOrWhiteSpace())
            {
                isAuthorized = false;
            };

            return isAuthorized;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var user = _dbContext.QuerySet<UserAccount>().FirstOrDefault(x => x.Login == Credentials.Login);

            if (user == null)
            {
                ErrorMessage = new() { Login = "Неверное имя пользователя" };
                return;
            }

            if (!Credentials.Password.VerifyPassword(user.PasswordHash, user.Salt))
            {
                ErrorMessage = new() { Password = "Неверный пароль" };
                return;
            }

            StorageHelper.SetUser(user.Login);

            RunCommand(x => Navigation.NavigateTo<EmployeeViewModel>(obj, WindowTypeEnum.Main));
        }
    }
}
