using Microsoft.EntityFrameworkCore;
using ProjectTest3.Data;
using ProjectTest3.Models;
using ProjectTest3.Repositories.Interface;


namespace ProjectTest3.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly ApplicationDbContext _context;

        public GenderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gender>> GetAllGendersAsync()
        {
            return await _context.Genders.ToListAsync();
        }

        //public async Task<Gender> GetGenderByIdAsync(int id)
        //{
        //    return await _context.Genders.FindAsync(id);
        //}
    }
}