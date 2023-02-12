using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class users10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
