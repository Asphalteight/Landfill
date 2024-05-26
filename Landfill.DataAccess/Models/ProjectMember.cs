namespace Landfill.DataAccess.Models
{
    public class ProjectMember : PersonEntity<int>
    {
        /// <summary>
        /// Заметки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Строительный проект
        /// </summary>
        public BuildProject BuildProject { get; set; }

        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public int BuildProjectId { get; set; }
    }
}
