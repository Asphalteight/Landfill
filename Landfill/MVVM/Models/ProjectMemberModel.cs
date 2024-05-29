using Landfill.DataAccess.Models;

namespace Landfill.MVVM.Models
{
    /// <summary>
    /// Модель участника проекта
    /// </summary>
    public class ProjectMemberModel : PersonModel
    {
        /// <summary>
        /// Заметки
        /// </summary>
        public string Description { get => _description; set { _description = value; OnModify(); } }

        /// <summary>
        /// Флаг выбран/не выбран 
        /// </summary>
        public bool IsSelected { get => _isSelected; set { _isSelected = value; OnPropertyChanged(); } }


        private string _description;
        private bool _isSelected;
    }
}
