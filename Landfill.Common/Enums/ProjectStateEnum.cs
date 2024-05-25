using System.ComponentModel;

namespace Landfill.Common.Enums
{
    /// <summary>
    /// Статус проекта
    /// </summary>
    public enum ProjectStateEnum
    {
        /// <summary>
        /// Создан
        /// </summary>
        [Description("Создан")]
        Created = 0,

        /// <summary>
        /// В работе
        /// </summary>
        [Description("В работе")]
        InProgress = 1,

        /// <summary>
        /// Завершен
        /// </summary>
        [Description("Завершен")]
        Done = 2
    }
}
