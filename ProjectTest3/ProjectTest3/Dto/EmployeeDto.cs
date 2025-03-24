using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProjectTest3.Dto
{
    public class EmployeeDto
    {
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

        [Required]
        [Display(Name = "Position")]
        public int PositionId { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        [Display(Name = "Programming Languages")]
        public List<int> SelectedLanguageIds { get; set; }

        // For radio buttons 
        
        [ValidateNever]
        public SelectList GenderOptions { get; set; }

        // For dropdown lists 
        
        [ValidateNever]
        public SelectList PositionOptions { get; set; }

        // For checkboxes
        [ValidateNever]
        public MultiSelectList LanguageOptions { get; set; }

        // Navigation properties for display
        [ValidateNever]
        public string GenderName { get; set; }
        [ValidateNever]
        public string PositionName { get; set; }
        [ValidateNever]
        public List<string> LanguageNames { get; set; }

        public EmployeeDto()
        {
            SelectedLanguageIds = new List<int>();
            LanguageNames = new List<string>();
        }
    }
}