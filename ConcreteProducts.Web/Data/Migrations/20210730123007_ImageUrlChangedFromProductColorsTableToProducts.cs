using Microsoft.EntityFrameworkCore.Migrations;

namespace ConcreteProducts.Web.Data.Migrations
{
    public partial class ImageUrlChangedFromProductColorsTableToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductColors");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductColors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
