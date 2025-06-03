using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.FMA.Migrations
{
    /// <inheritdoc />
    public partial class AcademicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicId",
                table: "FacultyMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicId",
                table: "FacultyMembers");
        }
    }
}
