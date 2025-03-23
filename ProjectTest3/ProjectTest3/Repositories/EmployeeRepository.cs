using Microsoft.EntityFrameworkCore;
using ProjectTest3.Data;
using ProjectTest3.Models;
using ProjectTest3.Repositories.Interface;


namespace ProjectTest3.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Include(e => e.Gender)
                .Include(e => e.Position)
                .Include(e => e.EmployeeLanguages)
                    .ThenInclude(el => el.Language)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            //return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> GetEmployeeWithDetailsAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Gender)
                .Include(e => e.Position)
                .Include(e => e.EmployeeLanguages)
                    .ThenInclude(el => el.Language)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            //_context.Entry(employee).State = EntityState.Modified;
            _context.Employees.Update(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }

        public async Task<bool> EmployeeExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeLanguagesAsync(int employeeId, List<int> languageIds)
        {
            // Remove existing language associations
            var existingLanguages = await _context.EmployeeLanguages
                .Where(el => el.EmployeeId == employeeId)
                .ToListAsync();

            _context.EmployeeLanguages.RemoveRange(existingLanguages);

            // Add new language associations
            if (languageIds != null)
            {
                foreach (var languageId in languageIds)
                {
                    await _context.EmployeeLanguages.AddAsync(new EmployeeLanguage
                    {
                        EmployeeId = employeeId,
                        LanguageId = languageId
                    });
                }
            }
        }
    }
}