using Landfill.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Landfill.DataAccess.Configuration
{
    public class UserAccountConfig : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("UserAccounts");

            builder.Property(x => x.Login).HasMaxLength(64).IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Salt).HasMaxLength(64).IsRequired();

            builder.HasOne(x => x.Employee).WithOne(x => x.UserAccount).HasForeignKey<Employee>(x => x.UserAccountId);
            builder.HasMany(x => x.Roles).WithOne(x => x.UserAccount).HasForeignKey(x => x.UserAccountId);

            builder.Navigation(x => x.Employee).AutoInclude();
        }
    }
}
