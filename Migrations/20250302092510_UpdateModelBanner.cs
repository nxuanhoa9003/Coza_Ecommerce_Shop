using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "Banner",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "Banner");
        }
    }
}
