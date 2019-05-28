using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_City_CityID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Country_CountryID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Locality_LocalityID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_PostCode_PostCodeID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Region_RegionID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AddressID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Address_CityID",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_CountryID",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_LocalityID",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_PostCodeID",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_RegionID",
                table: "Address");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionID",
                table: "Address",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PostCodeID",
                table: "Address",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LocalityID",
                table: "Address",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryID",
                table: "Address",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CityID",
                table: "Address",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RegionID",
                table: "Address",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PostCodeID",
                table: "Address",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "LocalityID",
                table: "Address",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryID",
                table: "Address",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CityID",
                table: "Address",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressID",
                table: "AspNetUsers",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityID",
                table: "Address",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryID",
                table: "Address",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_LocalityID",
                table: "Address",
                column: "LocalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_PostCodeID",
                table: "Address",
                column: "PostCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_RegionID",
                table: "Address",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_City_CityID",
                table: "Address",
                column: "CityID",
                principalTable: "City",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Country_CountryID",
                table: "Address",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Locality_LocalityID",
                table: "Address",
                column: "LocalityID",
                principalTable: "Locality",
                principalColumn: "LocalityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_PostCode_PostCodeID",
                table: "Address",
                column: "PostCodeID",
                principalTable: "PostCode",
                principalColumn: "PostCodeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Region_RegionID",
                table: "Address",
                column: "RegionID",
                principalTable: "Region",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AddressID",
                table: "AspNetUsers",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
