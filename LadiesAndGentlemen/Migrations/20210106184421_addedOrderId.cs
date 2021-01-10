using Microsoft.EntityFrameworkCore.Migrations;

namespace LadiesAndGentlemen.Migrations
{
    public partial class addedOrderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Cart_CartId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CartId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "CartId1",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Cart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartId1",
                table: "Order",
                column: "CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Cart_CartId1",
                table: "Order",
                column: "CartId1",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Cart_CartId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CartId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CartId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Cart");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartId",
                table: "Order",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Cart_CartId",
                table: "Order",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
