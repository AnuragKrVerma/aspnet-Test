namespace Project_Test.Models
{
    public class Subject
    {
        public int Id{ get; set; }
        public string Name{ get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
