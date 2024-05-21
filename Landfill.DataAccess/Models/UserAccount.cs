using System.Collections.Generic;

namespace Landfill.DataAccess.Models
{
    public class UserAccount : BaseEntity<int>
    {
        public string Login {  get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public Employee Employee { get; set; }

        public List<RoleToUser> Roles { get; set; } = [];
    }
}
