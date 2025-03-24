using Microsoft.EntityFrameworkCore;
using ProjectTest3.Data;
using ProjectTest3.Models;
using ProjectTest3.Repositories.Interface;


namespace ProjectTest3.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext _context;

        public PositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        //public async Task<Position> GetPositionByIdAsync(int id)
        //{
        //    return await _context.Positions.FindAsync(id);
        //}
    }
}