namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Модель с информацией о человеке
    /// </summary>
    public abstract class PersonEntity<TId> : BaseEntity<TId>
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }
    }
}
