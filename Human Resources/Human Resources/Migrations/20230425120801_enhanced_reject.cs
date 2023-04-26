using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Resources.Migrations
{
    /// <inheritdoc />
    public partial class enhanced_reject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefusalReason",
                table: "RejectedLeaves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefusalReason",
                table: "RejectedLeaves");
        }
    }
}
