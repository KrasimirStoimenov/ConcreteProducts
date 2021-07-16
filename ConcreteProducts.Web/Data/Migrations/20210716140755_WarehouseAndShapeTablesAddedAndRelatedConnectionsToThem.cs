using Microsoft.EntityFrameworkCore.Migrations;

namespace ConcreteProducts.Web.Data.Migrations
{
    public partial class WarehouseAndShapeTablesAddedAndRelatedConnectionsToThem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "Products",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shapes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Dimensions = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shapes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shapes_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_WarehouseId",
                table: "Products",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Shapes_WarehouseId",
                table: "Shapes",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Warehouses_WarehouseId",
                table: "Products",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Warehouses_WarehouseId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Shapes");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Products_WarehouseId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Products");
        }
    }
}
