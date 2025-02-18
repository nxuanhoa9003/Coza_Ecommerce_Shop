using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coza_Ecommerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class Update_ModelUserP2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedOtpAttempts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastOtpSent",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "OtpLockoutEnd",
                table: "Users",
                newName: "LastPasswordResetRequest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastPasswordResetRequest",
                table: "Users",
                newName: "OtpLockoutEnd");

            migrationBuilder.AddColumn<int>(
                name: "FailedOtpAttempts",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOtpSent",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
