using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoxFlorisMVCWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lid",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    voornaam = table.Column<string>(nullable: true),
                    achternaam = table.Column<string>(nullable: true),
                    geboortedatum = table.Column<DateTime>(nullable: false),
                    straat = table.Column<string>(nullable: true),
                    huisnummer = table.Column<string>(nullable: true),
                    postcode = table.Column<string>(nullable: true),
                    land = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    telefoon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bericht",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    titel = table.Column<string>(nullable: true),
                    inhoud = table.Column<string>(nullable: true),
                    datum = table.Column<DateTime>(nullable: false),
                    lidId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bericht", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bericht_Lid_lidId",
                        column: x => x.lidId,
                        principalTable: "Lid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Speedrun",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    snelheid = table.Column<double>(nullable: false),
                    datum = table.Column<DateTime>(nullable: false),
                    plank = table.Column<string>(nullable: true),
                    zeil = table.Column<string>(nullable: true),
                    lidId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speedrun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speedrun_Lid_lidId",
                        column: x => x.lidId,
                        principalTable: "Lid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_lidId",
                table: "Bericht",
                column: "lidId");

            migrationBuilder.CreateIndex(
                name: "IX_Speedrun_lidId",
                table: "Speedrun",
                column: "lidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bericht");

            migrationBuilder.DropTable(
                name: "Speedrun");

            migrationBuilder.DropTable(
                name: "Lid");
        }
    }
}
