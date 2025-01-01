using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueConstrainForNameInProductAndCategoryEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Brand",
                table: "Products",
                column: "Brand",
                unique: true,
                filter: "[Brand] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Brand",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Product_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Category_Name",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
