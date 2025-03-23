namespace ProjectTest2.Models.Helper
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeeLanguage> EmployeeLanguages { get; set; }
    }
}
