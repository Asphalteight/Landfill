using Landfill.DataAccess.Models;

namespace Landfill.MVVM.Models
{
    public class ProjectMemberModel : PersonModel
    {
        /// <summary>
        /// Заметки
        /// </summary>
        public string Description { get; set; }
    }
}
