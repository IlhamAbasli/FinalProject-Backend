using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class UpdatedTableRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Platforms_PlatformId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "MinSystemRequirements");

            migrationBuilder.DropTable(
                name: "RecomSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_Products_PlatformId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NewsImage1",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsImage2",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsImage3",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsMainImage",
                table: "News");

            migrationBuilder.CreateTable(
                name: "PlatformProduct",
                columns: table => new
                {
                    PlatformsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformProduct", x => new { x.PlatformsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_PlatformProduct_Platforms_PlatformsId",
                        column: x => x.PlatformsId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformProducts_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinOsVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinCpuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinMemory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinGpu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecomOsVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecomCpuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecomMemory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecomGpu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemRequirements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformProduct_ProductsId",
                table: "PlatformProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformProducts_PlatformId",
                table: "PlatformProducts",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformProducts_ProductId",
                table: "PlatformProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRequirements_ProductId",
                table: "SystemRequirements",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatformProduct");

            migrationBuilder.DropTable(
                name: "PlatformProducts");

            migrationBuilder.DropTable(
                name: "SystemRequirements");

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NewsImage1",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewsImage2",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewsImage3",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewsMainImage",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MinSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CpuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gpu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OsVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinSystemRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinSystemRequirements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecomSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CpuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gpu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OsVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecomSystemRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecomSystemRequirements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PlatformId",
                table: "Products",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_MinSystemRequirements_ProductId",
                table: "MinSystemRequirements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecomSystemRequirements_ProductId",
                table: "RecomSystemRequirements",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Platforms_PlatformId",
                table: "Products",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
