using Landfill.Abstractions;
using Landfill.MVVM.Models;
using Landfill.MVVM.ViewModels;

namespace Landfill.Services
{
    public interface IEmployeeService
    {
        public ObservableCollectionWithItemNotify<EmployeeInfoModel> Employees { get; set; }
        public int SelectedEmployeeIndex { get; set; }
        public bool CanEditEmployeeAdminRole { get; set; }
        public bool CanEditEmployeeManagerRole { get; set; }
    }

    public class EmployeeService : ViewModelBase, IEmployeeService
    {
        #region Поля и свойства

        private readonly INavigationService _navigation;
        private int _selectedEmployeeIndex;
        private int _previousSelectedEmployeeIndex;
        
        private ObservableCollectionWithItemNotify<EmployeeInfoModel> _employees;

        public bool CanEditEmployeeAdminRole { get; set; }
        public bool CanEditEmployeeManagerRole { get; set; }
        public int SelectedEmployeeIndex { get => _selectedEmployeeIndex; set { _selectedEmployeeIndex = value; SelectedItemChanged(); OnPropertyChanged(); } }
        public ObservableCollectionWithItemNotify<EmployeeInfoModel> Employees { get => _employees; set { _employees = value; ItemsChanged(); OnPropertyChanged(); } }

        #endregion

        public EmployeeService(INavigationService navigation)
        {
            _navigation = navigation;
        }

        public void ItemsChanged()
        {
            if (Employees.Count > 0)
            {
                SelectedEmployeeIndex = 0;
                Employees[SelectedEmployeeIndex].IsSelected = true;
                RunCommand(x => _navigation.NavigateItemPanelTo<EmployeeInfoViewModel>());
            }
        }

        private void SelectedItemChanged()
        {
            if (Employees.Count == 0)
            {
                RunCommand(x => _navigation.NavigateItemPanelTo<EmptyViewModel>());
                return;
            }
            if (Employees.Count > _previousSelectedEmployeeIndex && _previousSelectedEmployeeIndex >= 0)
            {
                Employees[_previousSelectedEmployeeIndex].IsSelected = false;
            }

            if (SelectedEmployeeIndex >= 0)
            {
                Employees[SelectedEmployeeIndex].IsSelected = true;
                RunCommand(x => _navigation.NavigateItemPanelTo<EmployeeInfoViewModel>());
            }

            _previousSelectedEmployeeIndex = SelectedEmployeeIndex;
        }
    }
}
