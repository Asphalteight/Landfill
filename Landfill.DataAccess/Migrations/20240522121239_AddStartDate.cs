using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Landfill.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStartDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "BuildProjects",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "BuildProjects");
        }
    }
}
