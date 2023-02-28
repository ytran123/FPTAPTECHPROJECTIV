using ProjectIVVer2.Model;

namespace ProjectIVVer2.Repository
{
    public interface IAdmin
    {
        Task<Admin> LoginAsync(string username, string password);
        Task<Admin> GetByIdAsync(int id);
        Task<IEnumerable<Admin>> GetAllAsync();
        Task<bool> CreateAsync(Admin admin);
        Task<bool> UpdateAsync(int id, Admin admin);
        Task<bool> DeleteAsync(int id);

    }
}
