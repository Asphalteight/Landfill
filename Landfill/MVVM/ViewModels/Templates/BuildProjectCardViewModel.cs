using Landfill.Abstractions;
using Landfill.Common.Enums;
using System;

namespace Landfill.MVVM.ViewModels.Templates
{
    public class BuildProjectCardViewModel : ViewModelBase
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; } = "Строительный проект по возведению здания государственного управления Республики Крым";

        public decimal Price { get; set; } = 777777777;

        public string Address { get; set; } = "г. Севастополь, ул. Иванова, д. 10";

        public DateTime? StartDate { get; set; }

        public DateTime PlanningCompletionDate { get; set; } = new DateTime(2026, 2, 5);

        public ProjectStateEnum State { get; set; } = ProjectStateEnum.InProgress;

        public string Customer { get; set; } = "ООО \"Новое время\"";

        public int EmployeeId { get; set; }

        public BuildProjectCardViewModel()
        {
             
        }
    }
}
