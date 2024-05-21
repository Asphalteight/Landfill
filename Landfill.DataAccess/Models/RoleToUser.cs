using Landfill.Common.Enums;

namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class RoleToUser : BaseEntity<int>
    {
        /// <summary>
        /// Роль
        /// </summary>
        public RoleEnum Role { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserAccountId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public UserAccount UserAccount { get; set; }
    }
}
