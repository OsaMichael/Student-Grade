using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGradeApp.Migrations
{
    /// <inheritdoc />
    public partial class updateCoursename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "CourseRegistrations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "CourseRegistrations");
        }
    }
}
