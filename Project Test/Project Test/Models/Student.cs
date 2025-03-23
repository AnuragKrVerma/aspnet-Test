using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_Test.Models
{
    public class Student
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Father{ get; set; }

        [Required]
        public IEnumerable<SelectListItem> Class { get; set; }

        [Required]
        public virtual ICollection<Subject> Subjects { get; set; }

        [Required]
        public string Gender { get; set; }

    }
}
