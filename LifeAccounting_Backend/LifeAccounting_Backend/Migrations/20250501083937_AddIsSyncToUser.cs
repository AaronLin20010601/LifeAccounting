using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeAccounting_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSyncToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSync",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSync",
                table: "Users");
        }
    }
}
