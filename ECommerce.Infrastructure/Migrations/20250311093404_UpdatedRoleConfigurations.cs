using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRoleConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_DeletedDate_HasFullPermission",
                table: "Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_HasFullPermission",
                table: "Roles",
                columns: new[] { "Name", "EntityName", "HasViewPermission", "HasCreateOrUpdatePermission", "HasFullPermission" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_HasFullPermission",
                table: "Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_DeletedDate_HasFullPermission",
                table: "Roles",
                columns: new[] { "Name", "EntityName", "HasViewPermission", "HasCreateOrUpdatePermission", "DeletedDate", "HasFullPermission" },
                unique: true,
                filter: "[DeletedDate] IS NOT NULL");
        }
    }
}
