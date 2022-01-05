using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sali",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrLocuri = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sali", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spectacols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataSpectacol = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PretBilet = table.Column<int>(type: "int", nullable: false),
                    LocuriVandute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spectacols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vanzari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpectacolId = table.Column<int>(type: "int", nullable: false),
                    NrBileteVandute = table.Column<int>(type: "int", nullable: false),
                    LocuriVandute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vanzari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vanzari_Spectacols_SpectacolId",
                        column: x => x.SpectacolId,
                        principalTable: "Spectacols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vanzari_SpectacolId",
                table: "Vanzari",
                column: "SpectacolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sali");

            migrationBuilder.DropTable(
                name: "Vanzari");

            migrationBuilder.DropTable(
                name: "Spectacols");
        }
    }
}
