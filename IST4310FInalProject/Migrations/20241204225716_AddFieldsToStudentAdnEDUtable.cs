using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IST4310FInalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToStudentAdnEDUtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "StudentInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "StudentInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "StudentInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Gpa",
                table: "Educations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "StudentInfos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "StudentInfos");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "StudentInfos");

            migrationBuilder.DropColumn(
                name: "Gpa",
                table: "Educations");
        }
    }
}
