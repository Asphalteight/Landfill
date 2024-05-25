using Landfill.Common.Enums;
using System;
using System.Collections.Generic;

namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Строительный проект
    /// </summary>
    public class BuildProject : BaseEntity<int>
    {
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
        public string Customer {  get; set; }

        /// <summary>
        /// Участники проекта
        /// </summary>
        public List<ProjectMember> Members { get; set; } = [];

        /// <summary>
        /// Сотрудник, создавший проект
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Идентификатор сотрудника-создателя
        /// </summary>
        public int EmployeeId { get; set; }
    }
}
