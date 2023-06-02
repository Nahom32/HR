using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Resources.Migrations
{
    /// <inheritdoc />
    public partial class added_no_of_days : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoOfDays",
                table: "RejectedLeaves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfDays",
                table: "Leaves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfDays",
                table: "ConfirmedLeaves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfDays",
                table: "RejectedLeaves");

            migrationBuilder.DropColumn(
                name: "NoOfDays",
                table: "Leaves");

            migrationBuilder.DropColumn(
                name: "NoOfDays",
                table: "ConfirmedLeaves");
        }
    }
}
