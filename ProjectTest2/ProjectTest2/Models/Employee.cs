using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ProjectTest2.Models.Helper;

namespace ProjectTest2.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        // Foreign key for Position
        public int PositionId { get; set; }
        public Position Position { get; set; }

        // Foreign key for Gender
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        // For languages in simple approach (comma-separated)
        public string LanguagesString { get; set; }

        // For many-to-many relationship with Language
        public ICollection<EmployeeLanguage> EmployeeLanguages { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime? UpdatedAt { get; set; }

        // Not mapped to database, used for form handling
        [NotMapped]
        public List<string> Languages
        {
            get => LanguagesString?.Split(',').ToList() ?? new List<string>();
            set => LanguagesString = value != null ? string.Join(",", value) : null;
        }
    }
}
