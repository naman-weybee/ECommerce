using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Country_Name",
                table: "Countries");

            migrationBuilder.CreateIndex(
                name: "IX_State_Name_CountryId",
                table: "States",
                columns: new[] { "Name", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Name_StateId",
                table: "Cities",
                columns: new[] { "Name", "StateId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_State_Name_CountryId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Country_Name",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_City_Name_StateId",
                table: "Cities");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                table: "Countries",
                column: "Name");
        }
    }
}
