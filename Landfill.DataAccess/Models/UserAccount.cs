using System.Collections.Generic;

namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Аккаунт пользователя
    /// </summary>
    public class UserAccount : BaseEntity<int>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login {  get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Соль, использованная при хэшировании пароля
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public List<RoleToUser> Roles { get; set; } = [];
    }
}
