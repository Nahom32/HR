using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Resources.Migrations
{
    /// <inheritdoc />
    public partial class first_encash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveType",
                table: "RejectedLeaves",
                newName: "LeaveTypesId");

            migrationBuilder.RenameColumn(
                name: "LeaveType",
                table: "Leaves",
                newName: "LeaveTypesId");

            migrationBuilder.RenameColumn(
                name: "LeaveType",
                table: "ConfirmedLeaves",
                newName: "LeaveTypesId");

            migrationBuilder.AddColumn<int>(
                name: "LeaveStatus",
                table: "Leaves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LeaveEncashments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveEncashments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveEncashments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Leave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RejectedLeaves_LeaveTypesId",
                table: "RejectedLeaves",
                column: "LeaveTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_LeaveTypesId",
                table: "Leaves",
                column: "LeaveTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmedLeaves_LeaveTypesId",
                table: "ConfirmedLeaves",
                column: "LeaveTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveEncashments_EmployeeId",
                table: "LeaveEncashments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmedLeaves_LeaveType_LeaveTypesId",
                table: "ConfirmedLeaves",
                column: "LeaveTypesId",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_LeaveType_LeaveTypesId",
                table: "Leaves",
                column: "LeaveTypesId",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedLeaves_LeaveType_LeaveTypesId",
                table: "RejectedLeaves",
                column: "LeaveTypesId",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmedLeaves_LeaveType_LeaveTypesId",
                table: "ConfirmedLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_LeaveType_LeaveTypesId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectedLeaves_LeaveType_LeaveTypesId",
                table: "RejectedLeaves");

            migrationBuilder.DropTable(
                name: "LeaveEncashments");

            migrationBuilder.DropTable(
                name: "LeaveType");

            migrationBuilder.DropIndex(
                name: "IX_RejectedLeaves_LeaveTypesId",
                table: "RejectedLeaves");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_LeaveTypesId",
                table: "Leaves");

            migrationBuilder.DropIndex(
                name: "IX_ConfirmedLeaves_LeaveTypesId",
                table: "ConfirmedLeaves");

            migrationBuilder.DropColumn(
                name: "LeaveStatus",
                table: "Leaves");

            migrationBuilder.RenameColumn(
                name: "LeaveTypesId",
                table: "RejectedLeaves",
                newName: "LeaveType");

            migrationBuilder.RenameColumn(
                name: "LeaveTypesId",
                table: "Leaves",
                newName: "LeaveType");

            migrationBuilder.RenameColumn(
                name: "LeaveTypesId",
                table: "ConfirmedLeaves",
                newName: "LeaveType");
        }
    }
}
