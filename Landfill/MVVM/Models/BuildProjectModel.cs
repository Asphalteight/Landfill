using Landfill.Abstractions;
using Landfill.Common.Enums;
using System;

namespace Landfill.MVVM.Models
{
    /// <summary>
    /// Модель для строительного проекта
    /// </summary>
    public class BuildProjectModel : ModifableObject
    {
        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get => _name; set { _name = value; OnModify(); OnPropertyChanged(); } }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get => _description; set { _description = value; OnModify(); } }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Price { get => _price; set { _price = value; OnModify(); } }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get => _address; set { _address = value; OnModify(); } }

        /// <summary>
        /// Дата начала работ
        /// </summary>
        public DateTime? StartDate { get => _startDate; set { _startDate = value; OnModify(); OnPropertyChanged(); } }

        /// <summary>
        /// Планируемая дата окончания работ
        /// </summary>
        public DateTime PlanningCompletionDate { get => _planningCompletionDate; set { _planningCompletionDate = value; OnModify(); OnPropertyChanged(); } }

        /// <summary>
        /// Фактическая дата окончания работ
        /// </summary>
        public DateTime? CompletionDate { get => _completionDate; set { _completionDate = value; OnModify(); OnPropertyChanged(); } }

        /// <summary>
        /// Статус
        /// </summary>
        public ProjectStateEnum State { get => _state; set { _state = value; OnModify(); } }

        /// <summary>
        /// Информация о заказчике (застройщике)
        /// </summary>
        public string Customer { get => _customer; set { _customer = value; OnModify(); } }

        /// <summary>
        /// Участники проекта
        /// </summary>
        public ModifableObservableCollection<ProjectMemberModel> Members { get; set; } = [];

        /// <summary>
        /// Сотрудник, добавивший проект
        /// </summary>
        public EmployeeInfoModel Employee { get => _employee; set { _employee = value;  } }

        /// <summary>
        /// Идентификатор сотрудника-создателя
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Флаг выбран/не выбран 
        /// </summary>
        public bool IsSelected { get => _isSelected; set { _isSelected = value; OnPropertyChanged(); } }


        private string _name;
        private string _description;
        private decimal _price;
        private string _address;
        private DateTime? _startDate;
        private DateTime _planningCompletionDate = DateTime.UtcNow.Date;
        private DateTime? _completionDate;
        private ProjectStateEnum _state;
        private string _customer;
        private EmployeeInfoModel _employee;

        private bool _isSelected;
    }
}
