using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.FMA.Migrations
{
    /// <inheritdoc />
    public partial class ContractReqFM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScanUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingLoads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    ScanUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingLoads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachingLoads_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_FacultyMemberId",
                table: "Contracts",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingLoads_FacultyMemberId",
                table: "TeachingLoads",
                column: "FacultyMemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "TeachingLoads");
        }
    }
}
