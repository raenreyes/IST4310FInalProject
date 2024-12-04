using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IST4310FInalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabasenotworking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInfo_AspNetUsers_UserId",
                table: "StudentInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentInfo",
                table: "StudentInfo");

            migrationBuilder.RenameTable(
                name: "StudentInfo",
                newName: "StudentInfos");

            migrationBuilder.RenameIndex(
                name: "IX_StudentInfo_UserId",
                table: "StudentInfos",
                newName: "IX_StudentInfos_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentInfos",
                table: "StudentInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInfos_AspNetUsers_UserId",
                table: "StudentInfos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInfos_AspNetUsers_UserId",
                table: "StudentInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentInfos",
                table: "StudentInfos");

            migrationBuilder.RenameTable(
                name: "StudentInfos",
                newName: "StudentInfo");

            migrationBuilder.RenameIndex(
                name: "IX_StudentInfos_UserId",
                table: "StudentInfo",
                newName: "IX_StudentInfo_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentInfo",
                table: "StudentInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInfo_AspNetUsers_UserId",
                table: "StudentInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
