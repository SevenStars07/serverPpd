using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "Vanzari",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaId",
                table: "Spectacols",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vanzari_SalaId",
                table: "Vanzari",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Spectacols_SalaId",
                table: "Spectacols",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spectacols_Sali_SalaId",
                table: "Spectacols",
                column: "SalaId",
                principalTable: "Sali",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vanzari_Sali_SalaId",
                table: "Vanzari",
                column: "SalaId",
                principalTable: "Sali",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spectacols_Sali_SalaId",
                table: "Spectacols");

            migrationBuilder.DropForeignKey(
                name: "FK_Vanzari_Sali_SalaId",
                table: "Vanzari");

            migrationBuilder.DropIndex(
                name: "IX_Vanzari_SalaId",
                table: "Vanzari");

            migrationBuilder.DropIndex(
                name: "IX_Spectacols_SalaId",
                table: "Spectacols");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "Vanzari");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "Spectacols");
        }
    }
}
