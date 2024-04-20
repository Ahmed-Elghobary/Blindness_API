using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blindness_API.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "localUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "medicalTests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateConducted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalTests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    TestID = table.Column<int>(type: "int", nullable: false),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalTestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Results_localUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "localUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_medicalTests_MedicalTestID",
                        column: x => x.MedicalTestID,
                        principalTable: "medicalTests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_MedicalTestID",
                table: "Results",
                column: "MedicalTestID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_PatientID",
                table: "Results",
                column: "PatientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "localUsers");

            migrationBuilder.DropTable(
                name: "medicalTests");
        }
    }
}
