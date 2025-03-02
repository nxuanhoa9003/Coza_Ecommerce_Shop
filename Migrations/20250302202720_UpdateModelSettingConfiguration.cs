using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelSettingConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SettingDescription",
                table: "SettingConfiguration");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleSub = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "SettingDescription",
                table: "SettingConfiguration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
