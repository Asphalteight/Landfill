using AutoMapper;
using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.Services;
using System.Linq;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class EmployeeProfileViewModel : ViewModelBase
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public INavigationService Navigation { get; set; }
        public IUserContextService UserContext { get; set; }

        public ICommand LogoutCommand { get; }
        public ICommand SaveProfileCommand { get; }
        public ICommand CancelChangesCommand { get; }
        public ICommand NavigateToEmployeesManagingCommand { get; }

        public EmployeeProfileViewModel(INavigationService navigation, IUserContextService userContext, IDbContext dbContext, IMapper mapper)
        {
            Navigation = navigation;
            _dbContext = dbContext;
            _mapper = mapper;
            UserContext = userContext;

            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            SaveProfileCommand = new ViewModelCommand(ExecuteSaveProfileCommand, CanExecuteSaveProfileCommand);
            CancelChangesCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeeViewModel>());
            NavigateToEmployeesManagingCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeesManagingViewModel>());
        }

        private bool CanExecuteSaveProfileCommand(object obj)
        {
            var isValid = UserContext.CurrentUser != null &&
                (new[] { UserContext.CurrentUser.FirstName, UserContext.CurrentUser.LastName, UserContext.CurrentUser.Phone }).All(x => !x.IsNullOrWhiteSpace());
            var isModified = UserContext.CurrentUser.IsModified;
            return isValid && isModified;
        }

        private void ExecuteSaveProfileCommand(object obj)
        {
            var employee = _dbContext.QuerySet<Employee>().FirstOrDefault(x => x.Id == UserContext.CurrentUser.Id);
            _mapper.Map(UserContext.CurrentUser, employee);

            _dbContext.SaveChanges();

            RunCommand(x => Navigation.NavigateTo<EmployeeViewModel>());
        }

        private void ExecuteLogoutCommand(object obj)
        {
            UserContext.ResetStoredUser();

            RunCommand(x => Navigation.NavigateTo<SignInViewModel>(obj, WindowTypeEnum.Login));
        }
    }
}
