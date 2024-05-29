using Landfill.Common.Enums;
using Landfill.DataAccess.Models;

namespace Landfill.MVVM.Models
{
    /// <summary>
    /// Модель сотрудника
    /// </summary>
    public class EmployeeInfoModel : PersonModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public RoleEnum[] Roles { get; set; }

        /// <summary>
        /// Флаг выбран/не выбран 
        /// </summary>
        public bool IsSelected { get => _isSelected; set { _isSelected = value; OnPropertyChanged(); } }


        private bool _isSelected;
    }
}
