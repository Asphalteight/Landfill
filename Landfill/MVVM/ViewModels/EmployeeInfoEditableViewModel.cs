using AutoMapper;
using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class EmployeeInfoEditableViewModel : ViewModelBase
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private bool _isAdmin;
        private bool _isManager;

        public IEmployeeService EmployeeService { get; set; }
        public IUserContextService UserContext {  get; set; }
        public INavigationService Navigation {  get; set; }
        public EmployeeInfoModel CurrentEmployee { get; set; }
        public bool IsAdmin { get => _isAdmin; set { _isAdmin = value; OnPropertyChanged(); } }
        public bool IsManager { get => _isManager; set { _isManager = value; OnPropertyChanged(); } }

        public ICommand SaveEmployeeCommand { get; }

        public EmployeeInfoEditableViewModel(IEmployeeService employeeService, IUserContextService userContext, IDbContext dbContext, IMapper mapper, INavigationService navigation)
        {
            EmployeeService = employeeService;
            UserContext = userContext;
            _dbContext = dbContext;
            _mapper = mapper;
            Navigation = navigation;

            SaveEmployeeCommand = new ViewModelCommand(ExecuteSaveEmployeeCommand, CanExecuteSaveEmployeeCommand);

            LoadEmployee();
        }

        private bool CanExecuteSaveEmployeeCommand(object obj)
        {
            var isValid = (new[] { CurrentEmployee.FirstName, CurrentEmployee.LastName, CurrentEmployee.Phone }).All(x => !x.IsNullOrWhiteSpace());
            var isModified = CurrentEmployee.IsModified;
            return isValid && isModified;
        }

        private void LoadEmployee()
        {
            CurrentEmployee = EmployeeService.Employees[EmployeeService.SelectedEmployeeIndex];
            IsAdmin = CurrentEmployee.Roles.Any(x => x == RoleEnum.Admin);
            IsManager = CurrentEmployee.Roles.Any(y => y == RoleEnum.Manager);

            var selectedEmployee = EmployeeService.Employees[EmployeeService.SelectedEmployeeIndex];
            EmployeeService.CanEditEmployeeAdminRole = UserContext.Permissions.EditEmployeeAdminRole && selectedEmployee.Id != UserContext.CurrentUser.Id;
            EmployeeService.CanEditEmployeeManagerRole = UserContext.Permissions.EditEmployeeManagerRole && selectedEmployee.Id != UserContext.CurrentUser.Id;
        }


        private void ExecuteSaveEmployeeCommand(object obj)
        {
            var roles = new List<RoleEnum>();
            if (IsAdmin)
            {
                roles.Add(RoleEnum.Admin);
            }
            if (IsManager)
            {
                roles.Add(RoleEnum.Manager);
            }
            CurrentEmployee.Roles = roles.ToArray();

            var employee = _dbContext.QuerySet<Employee>().FirstOrDefault(x => x.Id == CurrentEmployee.Id);
            _mapper.Map(CurrentEmployee, employee);

            _dbContext.SaveChanges();

            EmployeeService.Employees[EmployeeService.SelectedEmployeeIndex] = CurrentEmployee;
            EmployeeService.SelectedEmployeeIndex = EmployeeService.Employees.IndexOf(CurrentEmployee);

            if (CurrentEmployee.Id == UserContext.CurrentUser.Id)
            {
                UserContext.CurrentUser = CurrentEmployee;
            }
            RunCommand(x => Navigation.NavigateItemPanelTo<EmployeeInfoViewModel>());
        }
    }
}
