using System.Collections.Generic;

namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : PersonEntity<int>
    {
        /// <summary>
        /// Аккаунт
        /// </summary>
        public UserAccount UserAccount { get; set; }

        /// <summary>
        /// Идентификатор аккаунта
        /// </summary>
        public int UserAccountId { get; set; }

        /// <summary>
        /// Созданные строительные проекты
        /// </summary>
        public List<BuildProject> BuildProjects { get; set; } = [];

        /// <summary>
        /// Роли сотрудника
        /// </summary>
        public List<RoleToEmployee> Roles { get; set; } = [];
    }
}
