using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVAsp.NetProject.Migrations
{
    /// <inheritdoc />
    public partial class NewPropsAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Competencies",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Libraries",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Versioning",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Languages",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Libraries",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Versioning",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Competencies",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
