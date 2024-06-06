using AutoMapper;
using Landfill.Common.Enums;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using System.Linq;

namespace Landfill.Automapper
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Employee, EmployeeInfoModel>()
                .ForMember(x => x.IsNew, o => o.Ignore())
                .ForMember(x => x.IsSelected, o => o.Ignore())
                .ForMember(x => x.IsAdmin, o => o.MapFrom(x => x.Roles.Any(x => x.Role == RoleEnum.Admin)))
                .ForMember(x => x.IsManager, o => o.MapFrom(x => x.Roles.Any(x => x.Role == RoleEnum.Manager)))
                .ForMember(x => x.Roles, o => o.MapFrom(x => x.Roles.Select(x => x.Role)));

            CreateMap<EmployeeInfoModel, Employee>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.UserAccount, o => o.Ignore())
                .ForMember(x => x.UserAccountId, o => o.Ignore())
                .ForMember(x => x.BuildProjects, o => o.Ignore())
                .ForMember(x => x.CreatedOn, o => o.Ignore())
                .ForMember(x => x.UpdatedOn, o => o.Ignore())
                .ForMember(x => x.Roles, o => o.MapFrom(x => x.Roles.Select(role => new RoleToEmployee { Role = role })))
                ;

            CreateMap<CredentialsModel, CredentialsModel>();

            CreateMap<BuildProject, BuildProjectModel>()
                .ForMember(x => x.IsSelected, o => o.Ignore())
                .ForMember(x => x.IsNew, o => o.Ignore());

            CreateMap<BuildProjectModel, BuildProject>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.CreatedOn, o => o.Ignore())
                .ForMember(x => x.UpdatedOn, o => o.Ignore());

            CreateMap<ProjectMember, ProjectMemberModel>()
                .ForMember(x => x.IsSelected, o => o.Ignore())
                .ForMember(x => x.IsNew, o => o.Ignore());

            CreateMap<ProjectMemberModel, ProjectMember>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.BuildProject, o => o.Ignore())
                .ForMember(x => x.BuildProjectId, o => o.Ignore())
                .ForMember(x => x.CreatedOn, o => o.Ignore())
                .ForMember(x => x.UpdatedOn, o => o.Ignore());

            CreateMap<ProjectMemberModel, ProjectMemberModel>();
        }
    }
}
