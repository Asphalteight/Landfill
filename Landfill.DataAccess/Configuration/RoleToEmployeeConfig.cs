using Landfill.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Landfill.DataAccess.Configuration
{
    public class RoleToEmployeeConfig : IEntityTypeConfiguration<RoleToEmployee>
    {
        public void Configure(EntityTypeBuilder<RoleToEmployee> builder)
        {
            builder.ToTable("RolesToEmployees");

            builder.Property(x => x.Role).IsRequired();
        }
    }
}
