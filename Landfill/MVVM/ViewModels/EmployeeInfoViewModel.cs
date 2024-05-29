using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Linq;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class EmployeeInfoViewModel : ViewModelBase
    {
        private bool _isAdmin;
        private bool _isManager;

        public IEmployeeService EmployeeService { get; set; }
        public IUserContextService UserContext {  get; set; }
        public INavigationService Navigation {  get; set; }
        public bool IsAdmin { get => _isAdmin; set { _isAdmin = value; OnPropertyChanged(); } }
        public bool IsManager { get => _isManager; set { _isManager = value; OnPropertyChanged(); } }
        public EmployeeInfoModel CurrentEmployee { get; set; }

        public ICommand NavigateToEditEmployeeCommand { get; }

        public EmployeeInfoViewModel(IEmployeeService employeeService, IUserContextService userContext, INavigationService navigation)
        {
            EmployeeService = employeeService;
            UserContext = userContext;
            Navigation = navigation;

            NavigateToEditEmployeeCommand = new ViewModelCommand(x => Navigation.NavigateItemPanelTo<EmployeeInfoEditableViewModel>());

            LoadEmployee();
        }

        private void LoadEmployee()
        {
            CurrentEmployee = EmployeeService.Employees[EmployeeService.SelectedEmployeeIndex];
            IsAdmin = CurrentEmployee.Roles.Any(x => x == RoleEnum.Admin);
            IsManager = CurrentEmployee.Roles.Any(y => y == RoleEnum.Manager);
        }
    }
}
