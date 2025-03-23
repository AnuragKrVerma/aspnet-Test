using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTest2.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }

        // For dropdown
        [Required]
        public List<SelectListItem> PositionOptions { get; set; }

        //For language checkboxes
        [Required]
        public List<SelectListItem> AvailableLanguages { get; set; }
        public List<string> SelectedLanguages { get; set; } = new List<string>();

        // For radio buttons
        [Required]
        public List<SelectListItem> GenderOptions { get; set; }
    }
}