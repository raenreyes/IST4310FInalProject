using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IST4310FInalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillsTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolSkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_StudentInfos_StudentInfoId",
                        column: x => x.StudentInfoId,
                        principalTable: "StudentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_StudentInfoId",
                table: "Skills",
                column: "StudentInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
