using Microsoft.EntityFrameworkCore;
using ProjectTest2.Models.Helper;
using ProjectTest2.Models;

namespace ProjectTest2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<EmployeeLanguage> EmployeeLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Employee and Language
            modelBuilder.Entity<EmployeeLanguage>()
                .HasKey(el => new { el.EmployeeId, el.LanguageId });

            modelBuilder.Entity<EmployeeLanguage>()
                .HasOne(el => el.Employee)
                .WithMany(e => e.EmployeeLanguages)
                .HasForeignKey(el => el.EmployeeId);

            modelBuilder.Entity<EmployeeLanguage>()
                .HasOne(el => el.Language)
                .WithMany(l => l.EmployeeLanguages)
                .HasForeignKey(el => el.LanguageId);

            // Configure one-to-many relationship between Position and Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);

            // Configure one-to-many relationship between Gender and Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Gender)
                .WithMany(g => g.Employees)
                .HasForeignKey(e => e.GenderId);

            // Seed Position data
            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Name = "Developer" },
                new Position { Id = 2, Name = "Designer" },
                new Position { Id = 3, Name = "Manager" },
                new Position { Id = 4, Name = "HR" },
                new Position { Id = 5, Name = "QA" }
            );

            // Seed Gender data
            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Male" },
                new Gender { Id = 2, Name = "Female" },
                new Gender { Id = 3, Name = "Other" }
            );

            // Seed Languages data
            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "English" },
                new Language { Id = 2, Name = "Spanish" },
                new Language { Id = 3, Name = "French" },
                new Language { Id = 4, Name = "German" },
                new Language { Id = 5, Name = "Chinese" },
                new Language { Id = 6, Name = "Hindi" },
                new Language { Id = 7, Name = "Japanese" }
            );

            // Current timestamp for created date
            DateTime currentTime = DateTime.Parse("2025-03-18 21:30:47");

            // Seed demo Employees data
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "ABC",
                    Email = "ABC@example.com",
                    PhoneNumber = "1234567890",
                    PositionId = 1, // Developer
                    GenderId = 1, // Male
                    LanguagesString = "English,French",
                    CreatedAt = currentTime,
                    UpdatedAt = null
                },
                new Employee
                {
                    Id = 2,
                    Name = "DEF",
                    Email = "DEF@example.com",
                    PhoneNumber = "9876543210",
                    PositionId = 2, // Designer
                    GenderId = 2, // Female
                    LanguagesString = "English,Spanish,German",
                    CreatedAt = currentTime,
                    UpdatedAt = null
                },
                new Employee
                {
                    Id = 3,
                    Name = "HIJ",
                    Email = "HIJ@example.com",
                    PhoneNumber = "5554443333",
                    PositionId = 3, // Manager
                    GenderId = 1, // Male
                    LanguagesString = "English,Hindi",
                    CreatedAt = currentTime,
                    UpdatedAt = null
                },
                new Employee
                {
                    Id = 4,
                    Name = "KML",
                    Email = "KML@example.com",
                    PhoneNumber = "1112223333",
                    PositionId = 4, // HR
                    GenderId = 2, // Female
                    LanguagesString = "English,French,Chinese",
                    CreatedAt = currentTime,
                    UpdatedAt = null
                },
                new Employee
                {
                    Id = 5,
                    Name = "NOP",
                    Email = "NOP@example.com",
                    PhoneNumber = "4445556666",
                    PositionId = 5, // QA
                    GenderId = 3, // Other
                    LanguagesString = "English,Chinese,Japanese",
                    CreatedAt = currentTime,
                    UpdatedAt = null
                }
            );

            // Seed the join table for Employee-Language relationship
            modelBuilder.Entity<EmployeeLanguage>().HasData(
                // ABC languages
                new EmployeeLanguage { EmployeeId = 1, LanguageId = 1 }, // English
                new EmployeeLanguage { EmployeeId = 1, LanguageId = 3 }, // French

                // DEF languages
                new EmployeeLanguage { EmployeeId = 2, LanguageId = 1 }, // English
                new EmployeeLanguage { EmployeeId = 2, LanguageId = 2 }, // Spanish
                new EmployeeLanguage { EmployeeId = 2, LanguageId = 4 }, // German

                // HIJ languages
                new EmployeeLanguage { EmployeeId = 3, LanguageId = 1 }, // English
                new EmployeeLanguage { EmployeeId = 3, LanguageId = 6 }, // Hindi

                // KLM languages
                new EmployeeLanguage { EmployeeId = 4, LanguageId = 1 }, // English
                new EmployeeLanguage { EmployeeId = 4, LanguageId = 3 }, // French
                new EmployeeLanguage { EmployeeId = 4, LanguageId = 5 }, // Chinese

                // NOP languages
                new EmployeeLanguage { EmployeeId = 5, LanguageId = 1 }, // English
                new EmployeeLanguage { EmployeeId = 5, LanguageId = 5 }, // Chinese
                new EmployeeLanguage { EmployeeId = 5, LanguageId = 7 } // Japanese
            );
        }
    }
}
