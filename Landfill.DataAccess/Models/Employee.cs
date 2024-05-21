namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : PersonEntity<int>
    {
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Аккаунт
        /// </summary>
        public UserAccount UserAccount { get; set; }

        /// <summary>
        /// Идентификатор аккаунта
        /// </summary>
        public int UserAccountId { get; set; }
    }
}
