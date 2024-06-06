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
    /// <summary>
    /// Данные профиля пользователя
    /// </summary>
    public class EmployeeProfileViewModel : ViewModelBase
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public INavigationService Navigation { get; set; }
        public IUserContextService UserContext { get; set; }

        public EmployeeInfoModel EmployeeInfoModel { get; set;}

        public ICommand LogoutCommand { get; }
        public ICommand SaveProfileCommand { get; }
        public ICommand NavigateToMainCommand { get; }
        public ICommand NavigateToEmployeesManagingCommand { get; }

        public EmployeeProfileViewModel(INavigationService navigation, IUserContextService userContext, IDbContext dbContext, IMapper mapper)
        {
            Navigation = navigation;
            _dbContext = dbContext;
            _mapper = mapper;
            UserContext = userContext;

            LoadCurrentUser();
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            SaveProfileCommand = new ViewModelCommand(ExecuteSaveProfileCommand, CanExecuteSaveProfileCommand);
            NavigateToMainCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeeViewModel>());
            NavigateToEmployeesManagingCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeesManagingViewModel>());
        }

        private void LoadCurrentUser()
        {
            var employee = _dbContext.QuerySet<Employee>().FirstOrDefault(x => x.Id == UserContext.CurrentUser.Id);
            EmployeeInfoModel = _mapper.Map<EmployeeInfoModel>(employee);
        }

        private bool CanExecuteSaveProfileCommand(object obj)
        {
            if (EmployeeInfoModel != null)
            {
                var isValid = (new[] { EmployeeInfoModel.FirstName, EmployeeInfoModel.LastName, EmployeeInfoModel.Phone }).All(x => !x.IsNullOrWhiteSpace());
                var isModified = EmployeeInfoModel.IsModified;
                return isValid && isModified; 
            }
            return false;
        }

        private void ExecuteSaveProfileCommand(object obj)
        {
            var employee = _dbContext.QuerySet<Employee>().FirstOrDefault(x => x.Id == EmployeeInfoModel.Id);
            _mapper.Map(EmployeeInfoModel, employee);

            _dbContext.SaveChanges();

            UserContext.SetUser(employee.UserAccount);
            RunCommand(x => Navigation.NavigateTo<EmployeeProfileViewModel>());
        }

        private void ExecuteLogoutCommand(object obj)
        {
            UserContext.ResetStoredUser();

            RunCommand(x => Navigation.NavigateTo<SignInViewModel>(obj, WindowTypeEnum.Login));
        }
    }
}
