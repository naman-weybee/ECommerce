using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueinGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Gender_Name",
                table: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_RoleEntity_Id",
                table: "RoleEntities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleEntity_Name",
                table: "RoleEntities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Name",
                table: "Gender",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoleEntity_Id",
                table: "RoleEntities");

            migrationBuilder.DropIndex(
                name: "IX_RoleEntity_Name",
                table: "RoleEntities");

            migrationBuilder.DropIndex(
                name: "IX_Gender_Name",
                table: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Name",
                table: "Gender",
                column: "Name");
        }
    }
}
