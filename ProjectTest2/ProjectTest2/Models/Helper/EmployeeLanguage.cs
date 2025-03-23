namespace ProjectTest2.Models.Helper
{
    public class EmployeeLanguage
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
