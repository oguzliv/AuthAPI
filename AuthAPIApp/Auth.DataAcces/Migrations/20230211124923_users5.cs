using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class users5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleText",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoleText",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
