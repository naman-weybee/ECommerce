using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueConstraintToProductAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name_Brand_CategoryId",
                table: "Products",
                columns: new[] { "Name", "Brand", "CategoryId" },
                unique: true,
                filter: "[Brand] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SKU",
                table: "Products",
                column: "SKU",
                unique: true,
                filter: "[SKU] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name_ParentCategoryId",
                table: "Categories",
                columns: new[] { "Name", "ParentCategoryId" },
                unique: true,
                filter: "[ParentCategoryId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Name_Brand_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Product_SKU",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Category_Name_ParentCategoryId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}