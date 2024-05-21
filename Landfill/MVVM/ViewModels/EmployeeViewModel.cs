using Landfill.Abstractions;
using Landfill.DataAccess.Models;
using Landfill.DataAccess;
using Landfill.Services;
using System.Threading;
using System.Windows.Input;
using System.Linq;
using Landfill.Common.Helpers;
using Landfill.MVVM.Models;

namespace Landfill.MVVM.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        #region Проперти

        private readonly IDbContext _dbContext;
        private EmployeeInfoModel _currentUser;
        private INavigationService _navigation;

        public EmployeeInfoModel CurrentUser { get => _currentUser; set { _currentUser = value; OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public ICommand LogoutCommand { get; }

        #endregion

        public EmployeeViewModel(INavigationService navigation, IDbContext dbContext)
        {
            _navigation = navigation;
            _dbContext = dbContext;

            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);

            UpdateCurrentUser();
        }

        private void ExecuteLogoutCommand(object obj)
        {
            StorageHelper.ResetStoredUser();

            RunCommand(x => Navigation.NavigateTo<SignInViewModel>());
        }

        private void UpdateCurrentUser()
        {
            var currentUserLogin = Thread.CurrentPrincipal?.Identity?.Name;

            var employee = _dbContext.QuerySet<UserAccount>().FirstOrDefault(x => x.Login == currentUserLogin)?.Employee;
            if (employee != null)
            {
                CurrentUser = new EmployeeInfoModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    MiddleName = employee.MiddleName,
                    Phone = employee.Phone
                };
            }
        }
    }
}
