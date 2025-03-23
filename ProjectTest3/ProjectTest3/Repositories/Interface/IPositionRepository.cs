using ProjectTest3.Models;

namespace ProjectTest3.Repositories.Interface
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetAllPositionsAsync();
        Task<Position> GetPositionByIdAsync(int id);
    }
}
