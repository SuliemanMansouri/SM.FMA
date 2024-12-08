using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.FMA.Migrations
{
    /// <inheritdoc />
    public partial class FacultyMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacultyMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAwarded = table.Column<DateOnly>(type: "date", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Office = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyPositions_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyRanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PromotionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CriteriaMet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyRanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyRanks_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingHoursPerWeek = table.Column<int>(type: "int", nullable: false),
                    SabbaticalLeaveEveryYears = table.Column<int>(type: "int", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyTitles_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePublished = table.Column<DateOnly>(type: "date", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishingType = table.Column<int>(type: "int", nullable: false),
                    CoAuthors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_FacultyMemberId",
                table: "Certificates",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyPositions_FacultyMemberId",
                table: "FacultyPositions",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyRanks_FacultyMemberId",
                table: "FacultyRanks",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTitles_FacultyMemberId",
                table: "FacultyTitles",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_FacultyMemberId",
                table: "Publications",
                column: "FacultyMemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "FacultyPositions");

            migrationBuilder.DropTable(
                name: "FacultyRanks");

            migrationBuilder.DropTable(
                name: "FacultyTitles");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "FacultyMembers");
        }
    }
}
