namespace ProjectTest3.Models
{
    // Junction table for many-to-many relationship between Employee and Language
    public class EmployeeLanguage
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}