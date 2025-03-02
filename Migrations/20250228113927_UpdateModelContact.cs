using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Contact",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Contact");
        }
    }
}
