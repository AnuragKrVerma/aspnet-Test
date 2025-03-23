using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTest.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTest.Models
{
    public class Employee:User
    {
     
        public List<SelectListItem> Position { get; set; }

        public string SelectedPosition { get; set; }

        public List<CheckboxOption> LanguageCheckboxOptions { get; set; }

        public List<string> SelectedLanguageCheckboxOptions { get; set; }

       
        public List<RadioButtonBtn> GenderRadioButtonBtns { get; set; }

        public string SelectedGenderRadioButtonBtn { get; set; }

    }

   

  
}
