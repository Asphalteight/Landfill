using System;

namespace Landfill.DataAccess.Models
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class BaseEntity<TId> : BaseEntityWithTime
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public TId Id { get; set; }    
    }

    /// <summary>
    /// Сущность с временными отметками
    /// </summary>
    public abstract class BaseEntityWithTime
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
    }
}
