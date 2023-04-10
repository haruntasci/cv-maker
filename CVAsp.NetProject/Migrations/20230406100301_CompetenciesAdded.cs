using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVAsp.NetProject.Migrations
{
    /// <inheritdoc />
    public partial class CompetenciesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Competencies",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Competencies",
                table: "Users");
        }
    }
}
