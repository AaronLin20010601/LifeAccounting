using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeAccounting_Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFromPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromPrice",
                table: "ExchangeRates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FromPrice",
                table: "ExchangeRates",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
