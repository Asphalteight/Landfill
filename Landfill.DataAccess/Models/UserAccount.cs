namespace Landfill.DataAccess.Models
{
    public class UserAccount
    {
        public int Id { get; set; }

        public string Login {  get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }
    }
}
