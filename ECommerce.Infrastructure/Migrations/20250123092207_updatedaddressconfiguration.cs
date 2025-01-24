using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedaddressconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_CountryId_StateId_CityId_PostalCode",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Address",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryId",
                table: "Address",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_FirstName_LastName_CountryId_StateId_CityId_PostalCode_PhoneNumber",
                table: "Address",
                columns: new[] { "FirstName", "LastName", "CountryId", "StateId", "CityId", "PostalCode", "PhoneNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_CountryId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_FirstName_LastName_CountryId_StateId_CityId_PostalCode_PhoneNumber",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryId_StateId_CityId_PostalCode",
                table: "Address",
                columns: new[] { "CountryId", "StateId", "CityId", "PostalCode" },
                unique: true);
        }
    }
}