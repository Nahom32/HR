using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Resources.Migrations
{
    /// <inheritdoc />
    public partial class added_checkout_tracklists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckOutTrackLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInTrackListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOutTrackLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckOutTrackLists_CheckInTrackLists_CheckInTrackListId",
                        column: x => x.CheckInTrackListId,
                        principalTable: "CheckInTrackLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckOutTrackLists_CheckInTrackListId",
                table: "CheckOutTrackLists",
                column: "CheckInTrackListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckOutTrackLists");
        }
    }
}
