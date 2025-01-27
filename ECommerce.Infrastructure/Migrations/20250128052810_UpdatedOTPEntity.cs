using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOTPEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTP_Users_UserId",
                table: "OTP");

            migrationBuilder.RenameColumn(
                name: "ExpiredDate",
                table: "OTP",
                newName: "OTPExpiredDate");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "OTP",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiredDate",
                table: "OTP",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "OTP");

            migrationBuilder.DropColumn(
                name: "TokenExpiredDate",
                table: "OTP");

            migrationBuilder.RenameColumn(
                name: "OTPExpiredDate",
                table: "OTP",
                newName: "ExpiredDate");

            migrationBuilder.AddForeignKey(
                name: "FK_OTP_Users_UserId",
                table: "OTP",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
