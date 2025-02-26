using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ModifierDate",
                table: "Order",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Order",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "ReservedStock",
                table: "ProductVariants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VariantId",
                table: "OrderDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Order",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_VariantId",
                table: "OrderDetail",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductVariants_VariantId",
                table: "OrderDetail",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductVariants_VariantId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_VariantId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "ReservedStock",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Order",
                newName: "ModifierDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Order",
                newName: "CreateDate");

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
