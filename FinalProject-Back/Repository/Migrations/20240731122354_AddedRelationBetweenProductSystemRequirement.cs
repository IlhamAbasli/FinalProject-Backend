using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class AddedRelationBetweenProductSystemRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SystemRequirements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SystemRequirements_ProductId",
                table: "SystemRequirements",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemRequirements_Products_ProductId",
                table: "SystemRequirements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemRequirements_Products_ProductId",
                table: "SystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_SystemRequirements_ProductId",
                table: "SystemRequirements");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SystemRequirements");
        }
    }
}
