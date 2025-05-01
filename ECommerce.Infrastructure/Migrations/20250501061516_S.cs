using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class S : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_RoleEntities_RoleEntityId1",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId1",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleEntityId1",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleId1",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "RoleEntityId1",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "RolePermissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleEntityId1",
                table: "RolePermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId1",
                table: "RolePermissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 1, new Guid("13571357-1357-1357-1357-135713571357") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 2, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 3, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 4, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 8, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 9, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 10, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 11, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 13, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 14, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "RoleEntityId", "RoleId" },
                keyValues: new object[] { 15, new Guid("24682468-2468-2468-2468-246824682468") },
                columns: new[] { "RoleEntityId1", "RoleId1" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleEntityId1",
                table: "RolePermissions",
                column: "RoleEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId1",
                table: "RolePermissions",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_RoleEntities_RoleEntityId1",
                table: "RolePermissions",
                column: "RoleEntityId1",
                principalTable: "RoleEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId1",
                table: "RolePermissions",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
