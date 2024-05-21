using Landfill.DataAccess.Models.Enums;

namespace Landfill.DataAccess.Models
{
    public class RoleToUser : BaseEntity<int>
    {
        public RoleEnum Role { get; set; }

        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
