using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleEntityEnum4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_RoleEntities_RoleEntityId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleEntityId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleEntityId",
                table: "Roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleEntityId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleEntityId",
                table: "Roles",
                column: "RoleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_RoleEntities_RoleEntityId",
                table: "Roles",
                column: "RoleEntityId",
                principalTable: "RoleEntities",
                principalColumn: "Id");
        }
    }
}