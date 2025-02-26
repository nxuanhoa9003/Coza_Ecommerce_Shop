using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReservedStock",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReservedStock",
                table: "ProductVariants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
