using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedaddressconfiguration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_FirstName_LastName_UserId_CountryId_StateId_CityId_PostalCode_PhoneNumber",
                table: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Address_FirstName_LastName_UserId_CountryId_StateId_CityId_PostalCode_AdderessType_PhoneNumber",
                table: "Address",
                columns: new[] { "FirstName", "LastName", "UserId", "CountryId", "StateId", "CityId", "PostalCode", "AdderessType", "PhoneNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_FirstName_LastName_UserId_CountryId_StateId_CityId_PostalCode_AdderessType_PhoneNumber",
                table: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Address_FirstName_LastName_UserId_CountryId_StateId_CityId_PostalCode_PhoneNumber",
                table: "Address",
                columns: new[] { "FirstName", "LastName", "UserId", "CountryId", "StateId", "CityId", "PostalCode", "PhoneNumber" },
                unique: true);
        }
    }
}
