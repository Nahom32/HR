using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Resources.Migrations
{
    /// <inheritdoc />
    public partial class improved_leaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmedLeaves_Leaves_LeaveId",
                table: "ConfirmedLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectedLeaves_Leaves_LeaveId",
                table: "RejectedLeaves");

            migrationBuilder.RenameColumn(
                name: "LeaveId",
                table: "RejectedLeaves",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_RejectedLeaves_LeaveId",
                table: "RejectedLeaves",
                newName: "IX_RejectedLeaves_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "LeaveId",
                table: "ConfirmedLeaves",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfirmedLeaves_LeaveId",
                table: "ConfirmedLeaves",
                newName: "IX_ConfirmedLeaves_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmedLeaves_Employees_EmployeeId",
                table: "ConfirmedLeaves",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedLeaves_Employees_EmployeeId",
                table: "RejectedLeaves",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmedLeaves_Employees_EmployeeId",
                table: "ConfirmedLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectedLeaves_Employees_EmployeeId",
                table: "RejectedLeaves");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "RejectedLeaves",
                newName: "LeaveId");

            migrationBuilder.RenameIndex(
                name: "IX_RejectedLeaves_EmployeeId",
                table: "RejectedLeaves",
                newName: "IX_RejectedLeaves_LeaveId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "ConfirmedLeaves",
                newName: "LeaveId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfirmedLeaves_EmployeeId",
                table: "ConfirmedLeaves",
                newName: "IX_ConfirmedLeaves_LeaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmedLeaves_Leaves_LeaveId",
                table: "ConfirmedLeaves",
                column: "LeaveId",
                principalTable: "Leaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedLeaves_Leaves_LeaveId",
                table: "RejectedLeaves",
                column: "LeaveId",
                principalTable: "Leaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
