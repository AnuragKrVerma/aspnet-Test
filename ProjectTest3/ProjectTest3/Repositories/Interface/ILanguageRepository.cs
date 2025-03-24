using ProjectTest3.Models;

namespace ProjectTest3.Repositories.Interface
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
        //Task<Language> GetLanguageByIdAsync(int id);
        //Task<IEnumerable<Language>> GetLanguagesForEmployeeAsync(int employeeId);
    }
}
