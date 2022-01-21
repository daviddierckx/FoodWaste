using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AvansFysio.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beschikbaar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeschikbaarOpDieDag = table.Column<bool>(type: "bit", nullable: false),
                    Dag = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    BeginTijd = table.Column<TimeSpan>(type: "time(1)", nullable: false),
                    EindTijd = table.Column<TimeSpan>(type: "time(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beschikbaar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Leeftijd = table.Column<int>(type: "int", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    DiagnoseCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    MedewerkerOfStudent = table.Column<string>(type: "varchar(10)", nullable: false),
                    IntakeGedaanDoor = table.Column<string>(type: "varchar(200)", nullable: false),
                    onderSupervisieVan = table.Column<string>(type: "varchar(200)", nullable: true),
                    HoofdBehandelaar = table.Column<string>(type: "varchar(200)", nullable: false),
                    DatumAanmelding = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DatumOntslag = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Opmerkingen = table.Column<string>(type: "varchar(500)", nullable: false),
                    Behandeling = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Behandeling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    OefenzaalOfBehandel = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Bijzonderheden = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    BehandelingUitgevoerdDoor = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    BehandelingUitgevoerdDatum = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Behandeling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Behandeling_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Behandelplan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BehandelPlanNaam = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Duur = table.Column<int>(type: "int", nullable: false),
                    Hoeveel = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Behandelplan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Behandelplan_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opmerkingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opmerking = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Datum = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    OpmerkingenGemaaktDoor = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    ZichtbaarVoorPatiënt = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opmerkingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opmerkingen_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Behandeling_PatientId",
                table: "Behandeling",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Behandelplan_PatientId",
                table: "Behandelplan",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Opmerkingen_PatientId",
                table: "Opmerkingen",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Behandeling");

            migrationBuilder.DropTable(
                name: "Behandelplan");

            migrationBuilder.DropTable(
                name: "Beschikbaar");

            migrationBuilder.DropTable(
                name: "Opmerkingen");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
