using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AvansFysio.Migrations.Patient
{
    public partial class InialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Behandelplan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BehandelPlanNaam = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Duur = table.Column<int>(type: "int", nullable: false),
                    Hoeveel = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Behandelplan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Behandelplan_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Behandelplan_PatientId",
                table: "Behandelplan",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Behandelplan");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
