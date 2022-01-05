using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spectacols_Sali_SalaId",
                table: "Spectacols");

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "Spectacols",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Sali",
                columns: new[] { "Id", "NrLocuri" },
                values: new object[] { 1, 100 });

            migrationBuilder.InsertData(
                table: "Spectacols",
                columns: new[] { "Id", "DataSpectacol", "LocuriVandute", "PretBilet", "SalaId", "Sold", "Titlu" },
                values: new object[] { 1, new DateTime(2021, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 100, 1, 0, "S1" });

            migrationBuilder.InsertData(
                table: "Spectacols",
                columns: new[] { "Id", "DataSpectacol", "LocuriVandute", "PretBilet", "SalaId", "Sold", "Titlu" },
                values: new object[] { 2, new DateTime(2021, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 200, 1, 0, "S2" });

            migrationBuilder.InsertData(
                table: "Spectacols",
                columns: new[] { "Id", "DataSpectacol", "LocuriVandute", "PretBilet", "SalaId", "Sold", "Titlu" },
                values: new object[] { 3, new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 150, 1, 0, "S3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Spectacols_Sali_SalaId",
                table: "Spectacols",
                column: "SalaId",
                principalTable: "Sali",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spectacols_Sali_SalaId",
                table: "Spectacols");

            migrationBuilder.DeleteData(
                table: "Spectacols",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Spectacols",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Spectacols",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sali",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "Spectacols",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Spectacols_Sali_SalaId",
                table: "Spectacols",
                column: "SalaId",
                principalTable: "Sali",
                principalColumn: "Id");
        }
    }
}
