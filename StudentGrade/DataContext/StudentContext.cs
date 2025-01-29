using Microsoft.EntityFrameworkCore;
using StudentGradeApp.Models;

namespace StudentGradeApp.DataContext
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
