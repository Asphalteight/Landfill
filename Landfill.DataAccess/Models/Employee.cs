namespace Landfill.DataAccess.Models
{
    public class Employee : BaseEntity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Phone { get; set; }

        public UserAccount UserAccount { get; set; }
        public int UserAccountId { get; set; }
    }
}
