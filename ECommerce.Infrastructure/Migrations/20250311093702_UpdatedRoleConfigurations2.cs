using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRoleConfigurations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_Name",
                table: "Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Roles",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_Name",
                table: "Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Roles",
                column: "Name");
        }
    }
}
