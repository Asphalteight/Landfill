namespace Landfill.DataAccess.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationFullName { get; set; }

        public string OrganizationInn {  get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public UserAccount UserAccount { get; set; }
        public int UserAccountId { get; set; }


    }
}
