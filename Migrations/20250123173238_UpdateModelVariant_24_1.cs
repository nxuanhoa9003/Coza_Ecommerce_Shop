using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelVariant_24_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOptions_AttributeOptions_AttributeOptionId",
                table: "AttributeOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOptionValues_AttributeOptions_AttributeOptionId",
                table: "AttributeOptionValues");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceSale",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "AttributeOptionId",
                table: "AttributeOptions",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeOptions_AttributeOptionId",
                table: "AttributeOptions",
                newName: "IX_AttributeOptions_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "AttributeOptionValueIds",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceSale",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VariantName",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeOptionIds",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttributeOptionId",
                table: "AttributeOptionValues",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOptions_Product_ProductId",
                table: "AttributeOptions",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOptionValues_AttributeOptions_AttributeOptionId",
                table: "AttributeOptionValues",
                column: "AttributeOptionId",
                principalTable: "AttributeOptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOptions_Product_ProductId",
                table: "AttributeOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOptionValues_AttributeOptions_AttributeOptionId",
                table: "AttributeOptionValues");

            migrationBuilder.DropColumn(
                name: "AttributeOptionValueIds",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "PriceSale",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "VariantName",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "AttributeOptionIds",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "AttributeOptions",
                newName: "AttributeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeOptions_ProductId",
                table: "AttributeOptions",
                newName: "IX_AttributeOptions_AttributeOptionId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceSale",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AttributeOptionId",
                table: "AttributeOptionValues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOptions_AttributeOptions_AttributeOptionId",
                table: "AttributeOptions",
                column: "AttributeOptionId",
                principalTable: "AttributeOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOptionValues_AttributeOptions_AttributeOptionId",
                table: "AttributeOptionValues",
                column: "AttributeOptionId",
                principalTable: "AttributeOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
