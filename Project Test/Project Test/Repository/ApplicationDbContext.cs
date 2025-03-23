using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Project_Test.Models;

namespace Project_Test.Repository
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<Subject> Subjects { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Configure the many-to-many relationship between Student and Subject
        //    modelBuilder.Entity<Student>()
        //        .HasMany(s => s.Subjects)
        //        .WithMany(s => s.Students)
        //        .UsingEntity(j => j.ToTable("StudentSubject"));

        //    // Add demo data for Subjects
        //    modelBuilder.Entity<Subject>().HasData(
        //        new Subject { Id = 1, Name = "Mathematics" },
        //        new Subject { Id = 2, Name = "English" },
        //        new Subject { Id = 3, Name = "Science" },
        //        new Subject { Id = 4, Name = "History" }
        //    );

        //    // Add demo data for Students
        //    modelBuilder.Entity<Student>().HasData(
        //        new Student
        //        {
        //            Id = 1,
        //            Name = "ABC",
        //            Father = "ABCD",
        //            Class = new List<SelectListItem>
        //            {
        //                new SelectListItem { Value = "A", Text = "Class A" },
        //                new SelectListItem { Value = "B", Text = "Class B" }
        //            },
        //            Gender = "Male"
        //        },
        //        new Student
        //        {
        //            Id = 2,
        //            Name = "XYZ",
        //            Father = "XYZA",
        //            Class = new List<SelectListItem>
        //            {
        //                new SelectListItem { Value = "C", Text = "Class C" },
        //                new SelectListItem { Value = "D", Text = "Class D" }
        //            },
        //            Gender = "Female"
        //        }
        //    );

        //    // Add demo data for the many-to-many relationship
        //    modelBuilder.Entity<StudentSubject>().HasData(
        //        new { StudentId = 1, SubjectId = 1 },
        //        new { StudentId = 1, SubjectId = 2 },
        //        new { StudentId = 2, SubjectId = 2 },
        //        new { StudentId = 2, SubjectId = 3 }
        //    );
        //}
    }
}
