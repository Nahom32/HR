using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Resources.Migrations
{
    /// <inheritdoc />
    public partial class EducationalFieldRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationalFields_Employees_EmployeeId",
                table: "EducationalFields");

            migrationBuilder.DropIndex(
                name: "IX_EducationalFields_EmployeeId",
                table: "EducationalFields");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EducationalFields");

            migrationBuilder.AddColumn<int>(
                name: "EducationalFieldId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EducationalLevel",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EducationalFieldId",
                table: "Employees",
                column: "EducationalFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EducationalFields_EducationalFieldId",
                table: "Employees",
                column: "EducationalFieldId",
                principalTable: "EducationalFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EducationalFields_EducationalFieldId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EducationalFieldId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EducationalFieldId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EducationalLevel",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EducationalFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EducationalFields_EmployeeId",
                table: "EducationalFields",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalFields_Employees_EmployeeId",
                table: "EducationalFields",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
