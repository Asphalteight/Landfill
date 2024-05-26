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
                .ForMember(x => x.Position, m => m.MapFrom(x => "Сотрудник"))
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.UserAccount, o => o.Ignore())
                .ForMember(x => x.UserAccountId, o => o.Ignore())
                .ForMember(x => x.BuildProjects, o => o.Ignore())
                .ForMember(x => x.CreatedOn, o => o.Ignore())
                .ForMember(x => x.UpdatedOn, o => o.Ignore())
                ;

            CreateMap<CredentialsModel, CredentialsModel>();

            CreateMap<BuildProject, BuildProjectModel>()
                .ForMember(x => x.EditCommand, o => o.Ignore())
                .ForMember(x => x.RemoveCommand, o => o.Ignore())
                .ForMember(x => x.IsSelected, o => o.Ignore());

            CreateMap<BuildProjectModel, BuildProject>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.CreatedOn, o => o.Ignore())
                .ForMember(x => x.UpdatedOn, o => o.Ignore());

            CreateMap<ProjectMember, ProjectMemberModel>();
            CreateMap<ProjectMemberModel, ProjectMember>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.BuildProject, o => o.Ignore())
                .ForMember(x => x.BuildProjectId, o => o.Ignore())
                .ForMember(x => x.CreatedOn, o => o.Ignore())
                .ForMember(x => x.UpdatedOn, o => o.Ignore());

        }
    }
}
