using Landfill.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Landfill.DataAccess.Configuration
{
    public class BuildProjectConfig : IEntityTypeConfiguration<BuildProject>
    {
        public void Configure(EntityTypeBuilder<BuildProject> builder)
        {
            builder.ToTable("BuildProjects");

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Address).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Customer).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.PlanningCompletionDate).IsRequired();
            builder.Property(x => x.State).IsRequired();

            builder.HasMany(x => x.Members).WithOne(x => x.BuildProject).HasForeignKey(x => x.BuildProjectId);

            builder.Navigation(x => x.Employee).AutoInclude();
            builder.Navigation(x => x.Members).AutoInclude();
        }
    }
}
