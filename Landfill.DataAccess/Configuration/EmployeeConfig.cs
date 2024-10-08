﻿using Landfill.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Landfill.DataAccess.Configuration
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MiddleName).HasMaxLength(100);
            builder.Property(x => x.Phone).HasMaxLength(50);
            builder.HasMany(x => x.Roles).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);

            builder.HasMany(x => x.BuildProjects).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.Navigation(x => x.Roles).AutoInclude();
        }
    }
}
