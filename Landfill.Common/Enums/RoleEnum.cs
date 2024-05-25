using System.ComponentModel;

namespace Landfill.Common.Enums
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public enum RoleEnum
    {
        /// <summary>
        /// Сотрудник
        /// </summary>
        [Description("Сотрудник")]
        Employee = 0,

        /// <summary>
        /// Менеджер
        /// </summary>
        [Description("Менеджер")]
        Manager = 1,

        /// <summary>
        /// Администратор
        /// </summary>
        [Description("Администратор")]
        Admin = 2
    }
}
