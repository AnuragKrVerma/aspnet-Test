using Microsoft.EntityFrameworkCore;
using ProjectTest3.Models;
using System;

namespace ProjectTest3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<EmployeeLanguage> EmployeeLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship
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

            // Seed data for Genders
            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Male" },
                new Gender { Id = 2, Name = "Female" },
                new Gender { Id = 3, Name = "Other" }
            );

            // Seed data for Positions
            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Name = "Developer" },
                new Position { Id = 2, Name = "Designer" },
                new Position { Id = 3, Name = "Manager" },
                new Position { Id = 4, Name = "Tester" },
                new Position { Id = 5, Name = "Analyst" },
                new Position { Id = 6, Name = "HR" },
                new Position { Id = 7, Name = "Administrator" }
            );

            // Seed data for Languages
            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "C#" },
                new Language { Id = 2, Name = "Java" },
                new Language { Id = 3, Name = "Python" },
                new Language { Id = 4, Name = "JavaScript" },
                new Language { Id = 5, Name = "TypeScript" },
                new Language { Id = 6, Name = "Ruby" },
                new Language { Id = 7, Name = "PHP" },
                new Language { Id = 8, Name = "Go" },
                new Language { Id = 9, Name = "Swift" },
                new Language { Id = 10, Name = "Kotlin" }
            );

            // Seed data for Employees
            _ = modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "ABC",
                    Email = "ABC@example.com",
                    PhoneNumber = 1234567890,
                    PositionId = 1, // Developer
                    GenderId = 1    // Male
                },
                new Employee
                {
                    Id = 2,
                    Name = "DEF",
                    Email = "DEF@example.com",
                    PhoneNumber = 2345678901,
                    PositionId = 2, // Designer
                    GenderId = 2    // Female
                },
                new Employee
                {
                    Id = 3,
                    Name = "HIJ",
                    Email = "HIJ@example.com",
                    PhoneNumber = 3456789012,
                    PositionId = 3, // Manager
                    GenderId = 3    // Other
                }
            );

            // Seed data for EmployeeLanguages (many-to-many)
            modelBuilder.Entity<EmployeeLanguage>().HasData(
                // John Doe knows C#, JavaScript, and TypeScript
                new EmployeeLanguage { EmployeeId = 1, LanguageId = 1 },
                new EmployeeLanguage { EmployeeId = 1, LanguageId = 4 },
                new EmployeeLanguage { EmployeeId = 1, LanguageId = 5 },

                // Jane Smith knows JavaScript and Python
                new EmployeeLanguage { EmployeeId = 2, LanguageId = 4 },
                new EmployeeLanguage { EmployeeId = 2, LanguageId = 3 },

                // Alex Johnson knows Java and C#
                new EmployeeLanguage { EmployeeId = 3, LanguageId = 2 },
                new EmployeeLanguage { EmployeeId = 3, LanguageId = 1 }
            );
        }
    }
}