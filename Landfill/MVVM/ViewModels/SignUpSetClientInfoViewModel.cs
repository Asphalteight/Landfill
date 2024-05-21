using Landfill.Abstractions;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.Helpers;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class SignUpSetClientInfoViewModel : ViewModelBase
    {
        #region Проперти

        private readonly IDbContext _dbContext;
        private readonly ICredentialsService _userInfoService;
        private ErrorMessageModel _errorMessage;
        private ClientInfoModel _clientInfo = new();
        private INavigationService _navigation;

        public ErrorMessageModel ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(); } }
        public ClientInfoModel ClientInfo { get => _clientInfo; set { _clientInfo = value; OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public ICommand PreviousStepCommand { get; }
        public ICommand SignUpCommand { get; }

        #endregion

        public SignUpSetClientInfoViewModel(INavigationService navigation, IDbContext dbContext, ICredentialsService userInfoService)
        {
            Navigation = navigation;
            _dbContext = dbContext;
            _userInfoService = userInfoService;

            PreviousStepCommand = new ViewModelCommand(x => Navigation.NavigateTo<SignUpViewModel>());
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            var canExecute = !ClientInfo.FirstName.IsNullOrWhiteSpace();

            return canExecute;
        }

        private void ExecuteSignUpCommand(object obj)
        {
            var credentials = _userInfoService.Get();
            var salt = PasswordHelper.GenerateSalt();
            var passwordHash = credentials.Password.GetHash(salt);

            var user = new UserAccount()
            {
                Login = credentials.Login,
                PasswordHash = passwordHash,
                Salt = salt
            };
            var client = new Client 
            {
                FirstName = ClientInfo.FirstName
            };
            user.Client = client;

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            RunCommand(x => Navigation.NavigateTo<SignUpSuccessViewModel>());
        }
    }
}
