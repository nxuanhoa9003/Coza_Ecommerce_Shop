using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpadeModeWithProductAttributesValues_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValue_Attributes_AttributeId",
                table: "AttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_AttributeValue_ColorId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_AttributeValue_SizeId",
                table: "ProductVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValue",
                table: "AttributeValue");

            migrationBuilder.RenameTable(
                name: "AttributeValue",
                newName: "AttributeValues");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeValue_AttributeId",
                table: "AttributeValues",
                newName: "IX_AttributeValues_AttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeId",
                table: "AttributeValues",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_AttributeValues_ColorId",
                table: "ProductVariant",
                column: "ColorId",
                principalTable: "AttributeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_AttributeValues_SizeId",
                table: "ProductVariant",
                column: "SizeId",
                principalTable: "AttributeValues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeId",
                table: "AttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_AttributeValues_ColorId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_AttributeValues_SizeId",
                table: "ProductVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues");

            migrationBuilder.RenameTable(
                name: "AttributeValues",
                newName: "AttributeValue");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeValues_AttributeId",
                table: "AttributeValue",
                newName: "IX_AttributeValue_AttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValue",
                table: "AttributeValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValue_Attributes_AttributeId",
                table: "AttributeValue",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_AttributeValue_ColorId",
                table: "ProductVariant",
                column: "ColorId",
                principalTable: "AttributeValue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_AttributeValue_SizeId",
                table: "ProductVariant",
                column: "SizeId",
                principalTable: "AttributeValue",
                principalColumn: "Id");
        }
    }
}
