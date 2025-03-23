using System.ComponentModel.DataAnnotations;


namespace ProjectTest3.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public long PhoneNumber { get; set; }

        // Foreign key for Position
        public int PositionId { get; set; }

        // Foreign key for Gender
        public int GenderId { get; set; }

        // Navigation properties
        public Gender Gender { get; set; }
        public Position Position { get; set; }
        public ICollection<EmployeeLanguage> EmployeeLanguages { get; set; }
    }
}