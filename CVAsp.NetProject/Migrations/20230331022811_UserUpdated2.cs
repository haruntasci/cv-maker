using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVAsp.NetProject.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageFileName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true, defaultValue: "no-image.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageFileName",
                table: "Users");
        }
    }
}
