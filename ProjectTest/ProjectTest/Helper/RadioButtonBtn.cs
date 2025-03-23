using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjectTest.Helper
{
    [Keyless]
    public class RadioButtonBtn
    {
        //[Key]
        //public int Id { get; set; }

        public bool IsChecked { get; set; }

        public string BtnName { get; set; }

        public string BtnValue { get; set; }
    }
}
