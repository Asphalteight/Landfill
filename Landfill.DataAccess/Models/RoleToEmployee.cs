using Landfill.Common.Enums;

namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Роль сотрудника
    /// </summary>
    public class RoleToEmployee : BaseEntity<int>
    {
        /// <summary>
        /// Роль
        /// </summary>
        public RoleEnum Role { get; set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public Employee Employee { get; set; }
    }
}
