using Landfill.Abstractions;

namespace Landfill.MVVM.Models
{
    public class PermissionsModel : ObservableObject
    {
        public bool AddNewProjects { get => _addNewProjects; set { _addNewProjects = value; OnPropertyChanged(); } }
        public bool EditProjects { get => _editProjects; set { _editProjects = value; OnPropertyChanged(); } }
        public bool EditEmployee { get => _editEmployee; set { _editEmployee = value; OnPropertyChanged(); } }
        public bool EditEmployeeAdminRole { get => _editEmployeeAdminRole; set { _editEmployeeAdminRole = value; OnPropertyChanged(); } }
        public bool EditEmployeeManagerRole { get => _editEmployeeManagerRole; set { _editEmployeeManagerRole = value; OnPropertyChanged(); } }


        private bool _addNewProjects;
        private bool _editProjects;
        private bool _editEmployee;
        private bool _editEmployeeAdminRole;
        private bool _editEmployeeManagerRole;
    }
}
