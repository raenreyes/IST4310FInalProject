using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IST4310FInalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToEducationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Educations");
        }
    }
}
