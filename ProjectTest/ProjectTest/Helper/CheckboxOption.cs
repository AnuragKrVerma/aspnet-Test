using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjectTest.Helper
{
    [Keyless]
    public class CheckboxOption
    {

        //[Key]
        //public int Id { get; set; }

        public bool IsChecked { get; set; }

        public string OptionName { get; set; }

        public string OptionValue { get; set; }
        
    }
}
