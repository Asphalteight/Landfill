using Landfill.Abstractions;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class EmployeeInfoViewModel : ViewModelBase
    {
        public IEmployeeService EmployeeService { get; set; }
        public IUserContextService UserContext {  get; set; }
        public INavigationService Navigation {  get; set; }
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
        }
    }
}
