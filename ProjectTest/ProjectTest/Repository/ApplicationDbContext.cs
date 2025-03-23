using Microsoft.EntityFrameworkCore;
using ProjectTest.Models;
using ProjectTest.Helper;

namespace ProjectTest.Repository
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):DbContext(options)
    {
       public DbSet<Employee> Employees { get; set; }
       public DbSet<Positions> positions { get; set; }

       public DbSet<CheckboxOption> CheckboxOptions { get; set; }

        public DbSet<RadioButtonBtn> RadioButtonBtns { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

          modelBuilder.Entity<Positions>().HasData(
            new Positions
            {
                PositionId = "SDE",
                PositionName = "Software Developer"
            },
            new Positions
            {
                PositionId = "SDET",
                PositionName = "Software Development Engineer in Test"
            },
            new Positions
            {
                PositionId = "PM",
                PositionName = "Project Manager"
            },
            new Positions
            {
                PositionId = "QA",
                PositionName = "Quality Assurance"
            },
            new Positions
            {
                PositionId = "BA",
                PositionName = "Business Analyst"
            }
          );

          modelBuilder.Entity<CheckboxOption>().HasData(
            new CheckboxOption
            {
               IsChecked = false,
                OptionName = "English",
                OptionValue = "Eng"
            },
            new CheckboxOption
            {
                IsChecked = false,
                OptionName = "Spanish",
                OptionValue = "Spa"
            },
            new CheckboxOption
            {
                IsChecked = false,
                OptionName = "French",
                OptionValue = "Fre"
            },
            new CheckboxOption
            {
                IsChecked = false,
                OptionName = "German",
                OptionValue = "Ger"
            },
            new CheckboxOption
            {
                IsChecked = false,
                OptionName ="Hindi",
                OptionValue = "Hin"
            }
          );

          modelBuilder.Entity<RadioButtonBtn>().HasData(
            new RadioButtonBtn
            {
                IsChecked = false,
                BtnName = "Male",
                BtnValue = "M"
            },
            new RadioButtonBtn
            {
                IsChecked = false,
                BtnName = "Female",
                BtnValue = "F"
            },
            new RadioButtonBtn
            {
                IsChecked = false,
                BtnName = "Other",
                BtnValue = "O"
            }
          );



        }
    }
}
