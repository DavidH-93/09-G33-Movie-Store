using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.Migrations
{
    public partial class BigBoyOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Movie_MovieID",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_MovieID",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieID",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "OrderItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "Order",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieID",
                table: "OrderItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Order",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_MovieID",
                table: "OrderItem",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Movie_MovieID",
                table: "OrderItem",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
