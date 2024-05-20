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

            builder.Property(x => x.PasswordHash).HasMaxLength(64);
            builder.Property(x => x.Salt).HasMaxLength(64);
            builder.HasOne(x => x.Client).WithOne(x => x.UserAccount).HasForeignKey<Client>(x => x.UserAccountId);
            builder.Navigation(x => x.Client).AutoInclude();
        }
    }
}
