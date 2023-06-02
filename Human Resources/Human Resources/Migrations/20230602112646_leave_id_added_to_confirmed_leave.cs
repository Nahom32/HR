using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Resources.Migrations
{
    /// <inheritdoc />
    public partial class leave_id_added_to_confirmed_leave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveId",
                table: "RejectedLeaves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeaveId",
                table: "ConfirmedLeaves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveId",
                table: "RejectedLeaves");

            migrationBuilder.DropColumn(
                name: "LeaveId",
                table: "ConfirmedLeaves");
        }
    }
}
