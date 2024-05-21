using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Landfill.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeBuildProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "BuildProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BuildProjects_EmployeeId",
                table: "BuildProjects",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildProjects_Employees_EmployeeId",
                table: "BuildProjects",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildProjects_Employees_EmployeeId",
                table: "BuildProjects");

            migrationBuilder.DropIndex(
                name: "IX_BuildProjects_EmployeeId",
                table: "BuildProjects");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "BuildProjects");
        }
    }
}
