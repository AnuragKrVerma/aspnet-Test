using Microsoft.EntityFrameworkCore;
using ProjectTest3.Data;
using ProjectTest3.Models;
using ProjectTest3.Repositories.Interface;

namespace ProjectTest3.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;

        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<Language> GetLanguageByIdAsync(int id)
        {
            return await _context.Languages.FindAsync(id);
        }

        public async Task<IEnumerable<Language>> GetLanguagesForEmployeeAsync(int employeeId)
        {
            return await _context.EmployeeLanguages
                .Where(el => el.EmployeeId == employeeId)
                .Select(el => el.Language)
                .ToListAsync();
        }
    }
}