using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleEntityEnum2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleEntityId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntities", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_RoleEntities_RoleEntityId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "RoleEntities");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleEntityId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleEntityId",
                table: "Roles");
        }
    }
}
