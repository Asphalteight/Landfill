using AutoMapper;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;

namespace Landfill.Automapper
{
    public class ModelToModelMapProfile : Profile
    {
        public ModelToModelMapProfile()
        {
            CreateMap<Employee, EmployeeInfoModel>();
            CreateMap<EmployeeInfoModel, Employee>()
                .ForMember(x => x.Position, m => m.MapFrom(x => "Сотрудник"));

            CreateMap<CredentialsModel, CredentialsModel>();

            CreateMap<BuildProject, BuildingProjectModel>();
        }
    }
}
