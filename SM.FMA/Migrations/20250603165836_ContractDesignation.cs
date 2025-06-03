using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.FMA.Migrations
{
    /// <inheritdoc />
    public partial class ContractDesignation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyTitles");

            migrationBuilder.AddColumn<int>(
                name: "ContractualDesignation",
                table: "FacultyMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractualDesignation",
                table: "FacultyMembers");

            migrationBuilder.CreateTable(
                name: "FacultyTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SabbaticalLeaveEveryYears = table.Column<int>(type: "int", nullable: false),
                    TeachingHoursPerWeek = table.Column<int>(type: "int", nullable: false),
                    TitleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTitles_FacultyMemberId",
                table: "FacultyTitles",
                column: "FacultyMemberId");
        }
    }
}
