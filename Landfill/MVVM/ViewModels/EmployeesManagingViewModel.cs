using AutoMapper;
using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Linq;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class EmployeesManagingViewModel : ViewModelBase
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        
        private EmployeeInfoModel _currentEmployee;

        public IUserContextService UserContext { get; set; }
        public INavigationService Navigation { get; set; }
        public IEmployeeService EmployeeService { get; set; }

        public EmployeeInfoModel CurrentEmployee { get => _currentEmployee; set { _currentEmployee = value; OnPropertyChanged(); } }
        

        public ICommand NavigateToEmployeeProfileCommand { get; }
        public ICommand NavigateToMainCommand { get; }
        public ICommand LogoutCommand { get; }

        public EmployeesManagingViewModel(IUserContextService userContext, INavigationService navigation, IDbContext dbContext, IMapper mapper, IEmployeeService employeeService)
        {
            UserContext = userContext;
            Navigation = navigation;
            _dbContext = dbContext;
            _mapper = mapper;
            EmployeeService = employeeService;

            NavigateToEmployeeProfileCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeeProfileViewModel>());
            NavigateToMainCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeeViewModel>());
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);

            LoadEmployees();
        }

        private void LoadEmployees()
        {
            var employees = _dbContext.QuerySet<Employee>().ToList();
            EmployeeService.Employees = _mapper.Map<ObservableCollectionWithItemNotify<EmployeeInfoModel>>(employees);
        }

        private void ExecuteLogoutCommand(object obj)
        {
            UserContext.ResetStoredUser();

            RunCommand(x => Navigation.NavigateTo<SignInViewModel>(obj, WindowTypeEnum.Login));
        }
    }
}
