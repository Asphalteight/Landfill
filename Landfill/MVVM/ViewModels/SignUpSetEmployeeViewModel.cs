using AutoMapper;
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
    public class SignUpSetEmployeeViewModel : ViewModelBase
    {
        #region Проперти

        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
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

        public SignUpSetEmployeeViewModel(INavigationService navigation, IDbContext dbContext, ICredentialsService userInfoService, IMapper mapper)
        {
            Navigation = navigation;
            _dbContext = dbContext;
            _credentialsService = userInfoService;
            _mapper = mapper;

            PreviousStepCommand = new ViewModelCommand(x => Navigation.NavigateTo<SignUpViewModel>());
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            var canExecute = true;
            if ((new[] { EmployeeInfo.FirstName, EmployeeInfo.LastName, EmployeeInfo.Phone }).Any(x => x.IsNullOrWhiteSpace()))
            {
                canExecute = false;
            }

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
                Salt = salt,
                Employee = _mapper.Map<Employee>(EmployeeInfo)
            };
            user.Roles.Add(new RoleToUser { Role = RoleEnum.Employee });

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            _credentialsService.Set(new CredentialsModel());
            RunCommand(x => Navigation.NavigateTo<SignUpSuccessViewModel>());
        }
    }
}
