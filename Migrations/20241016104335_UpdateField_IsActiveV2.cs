using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateField_IsActiveV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Post",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "New",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Category",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Post",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "New",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Category",
                newName: "isActive");
        }
    }
}
