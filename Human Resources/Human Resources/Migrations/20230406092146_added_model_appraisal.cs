using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Human_Resources.Migrations
{
    /// <inheritdoc />
    public partial class added_model_appraisal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appraisals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PunctualityId = table.Column<int>(type: "int", nullable: false),
                    TimelinessId = table.Column<int>(type: "int", nullable: false),
                    GroupWorkId = table.Column<int>(type: "int", nullable: false),
                    TechnicalSkillsId = table.Column<int>(type: "int", nullable: false),
                    CollaborativeSkillsId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraisals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appraisals_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appraisals_Grades_CollaborativeSkillsId",
                        column: x => x.CollaborativeSkillsId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appraisals_Grades_GroupWorkId",
                        column: x => x.GroupWorkId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appraisals_Grades_PunctualityId",
                        column: x => x.PunctualityId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appraisals_Grades_TechnicalSkillsId",
                        column: x => x.TechnicalSkillsId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appraisals_Grades_TimelinessId",
                        column: x => x.TimelinessId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_CollaborativeSkillsId",
                table: "Appraisals",
                column: "CollaborativeSkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_EmployeeId",
                table: "Appraisals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_GroupWorkId",
                table: "Appraisals",
                column: "GroupWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_PunctualityId",
                table: "Appraisals",
                column: "PunctualityId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_TechnicalSkillsId",
                table: "Appraisals",
                column: "TechnicalSkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_TimelinessId",
                table: "Appraisals",
                column: "TimelinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appraisals");
        }
    }
}
