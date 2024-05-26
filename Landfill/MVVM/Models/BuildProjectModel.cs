using Landfill.Abstractions;
using Landfill.Common.Enums;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Landfill.MVVM.Models
{
    /// <summary>
    /// Модель для строительного проекта
    /// </summary>
    public class BuildProjectModel : ObservableObject
    {
        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Дата начала работ
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Планируемая дата окончания работ
        /// </summary>
        public DateTime PlanningCompletionDate { get; set; }

        /// <summary>
        /// Фактическая дата окончания работ
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public ProjectStateEnum State { get; set; }

        /// <summary>
        /// Информация о заказчике (застройщике)
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Участники проекта
        /// </summary>
        public List<ProjectMemberModel> Members { get; set; } = [];

        /// <summary>
        /// Сотрудник, добавивший проект
        /// </summary>
        public EmployeeInfoModel Employee { get; set; }

        /// <summary>
        /// Идентификатор сотрудника-создателя
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedOn { get; set; }

        private ICommand _editCommand;
        public ICommand EditCommand { get => _editCommand; set { _editCommand = value; OnPropertyChanged(); } }

        private ICommand _removeCommand;
        public ICommand RemoveCommand { get => _removeCommand; set { _removeCommand = value; OnPropertyChanged(); } }

        private bool _isSelected;
        public bool IsSelected { get => _isSelected; set { _isSelected = value; OnPropertyChanged(); } }
    }
}
