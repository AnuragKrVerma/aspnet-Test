using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProjectTest.Helper
{
    [Keyless]
    public class Positions
    {
        //[Key]
        //public int Id { get; set; }

        public string PositionId { get; set; }

        public string PositionName { get; set; }
    }
}
