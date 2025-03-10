using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Gender_Name",
                table: "Roles",
                newName: "IX_Role_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Gender_Id",
                table: "Roles",
                newName: "IX_Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_EntityName",
                table: "Roles",
                column: "EntityName");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_DeletedDate_HasFullPermission",
                table: "Roles",
                columns: new[] { "Name", "EntityName", "HasViewPermission", "HasCreateOrUpdatePermission", "DeletedDate", "HasFullPermission" },
                unique: true,
                filter: "[DeletedDate] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_EntityName",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_DeletedDate_HasFullPermission",
                table: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Name",
                table: "Roles",
                newName: "IX_Gender_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Id",
                table: "Roles",
                newName: "IX_Gender_Id");
        }
    }
}