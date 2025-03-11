using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleEntityEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_EntityName",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_HasFullPermission",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EntityName",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "RoleEntity",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name_RoleEntityId_HasViewPermission_HasCreateOrUpdatePermission_HasDeletePermission_HasFullPermission",
                table: "Roles",
                columns: new[] { "Name", "RoleEntity", "HasViewPermission", "HasCreateOrUpdatePermission", "HasDeletePermission", "HasFullPermission" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleEntityId",
                table: "Roles",
                column: "RoleEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_Name_RoleEntityId_HasViewPermission_HasCreateOrUpdatePermission_HasDeletePermission_HasFullPermission",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Role_RoleEntityId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleEntity",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "EntityName",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Role_EntityName",
                table: "Roles",
                column: "EntityName");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_HasFullPermission",
                table: "Roles",
                columns: new[] { "Name", "EntityName", "HasViewPermission", "HasCreateOrUpdatePermission", "HasFullPermission" },
                unique: true);
        }
    }
}