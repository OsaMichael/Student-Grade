using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGradeApp.Migrations
{
    /// <inheritdoc />
    public partial class courseUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseUnit",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseUnit",
                table: "Courses");
        }
    }
}
