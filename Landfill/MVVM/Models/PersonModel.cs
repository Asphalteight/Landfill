using Landfill.Abstractions;

namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Модель с информацией о человеке
    /// </summary>
    public class PersonModel : ModifableObject
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get => _firstName; set { _firstName = value; OnModify(); } }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get => _lastName; set { _lastName = value; OnModify(); } }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get => _middleName; set { _middleName = value; OnModify(); } }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get => _phone; set { _phone = value; OnModify(); } }


        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _phone;
    }
}
