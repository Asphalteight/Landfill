using Landfill.Common.Enums;
using System;

namespace Landfill.MVVM.Models
{
    /// <summary>
    /// Модель для строительного проекта
    /// </summary>
    public class BuildingProjectModel
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
        /// Статус
        /// </summary>
        public ProjectStateEnum State { get; set; }

        /// <summary>
        /// Информация о заказчике (застройщике)
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Идентификатор сотрудника-создателя
        /// </summary>
        public int EmployeeId { get; set; }
    }
}
