using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatformProduct");

            migrationBuilder.DropTable(
                name: "PlatformProducts");

            migrationBuilder.CreateTable(
                name: "PlatformSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatformId = table.Column<int>(type: "int", nullable: false),
                    SystemRequirementId = table.Column<int>(type: "int", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformSystemRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformSystemRequirements_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformSystemRequirements_SystemRequirements_SystemRequirementId",
                        column: x => x.SystemRequirementId,
                        principalTable: "SystemRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformSystemRequirements_PlatformId",
                table: "PlatformSystemRequirements",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformSystemRequirements_SystemRequirementId",
                table: "PlatformSystemRequirements",
                column: "SystemRequirementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatformSystemRequirements");

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
                    PlatformId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
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
        }
    }
}
