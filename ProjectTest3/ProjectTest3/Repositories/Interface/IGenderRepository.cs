using ProjectTest3.Models;

namespace ProjectTest3.Repositories.Interface
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GetAllGendersAsync();
        Task<Gender> GetGenderByIdAsync(int id);
    }
}
