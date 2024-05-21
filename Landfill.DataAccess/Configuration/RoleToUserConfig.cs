using Landfill.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Landfill.DataAccess.Configuration
{
    public class RoleToUserConfig : IEntityTypeConfiguration<RoleToUser>
    {
        public void Configure(EntityTypeBuilder<RoleToUser> builder)
        {
            builder.ToTable("RolesToUsers");

            builder.Property(x => x.Role).IsRequired();
        }
    }
}
