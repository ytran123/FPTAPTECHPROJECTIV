using ProjectIVVer2.Model;
using System.Data;
using ProjectIVVer2.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectIVVer2.Services
{
    public class AdminService : Repository.IAdmin
    {

        private readonly EcommerceDBContext _dbContext;

        public AdminService(EcommerceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> LoginAsync(string username, string password)
        {
            var admin = _dbContext.Admins.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
            return admin;
        }

        public async Task<Admin> GetByIdAsync(int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);
            return admin;
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            var admins = await _dbContext.Admins.ToListAsync();
            return admins;
        }

        public async Task<bool> CreateAsync(Admin admin)
        {
            await _dbContext.Admins.AddAsync(admin);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAdmin(string username)
        {
            try
            {
                var admin = await _dbContext.Admins.FirstOrDefaultAsync(x => x.Username == username);

                if (admin != null)
                {
                    _dbContext.Admins.Remove(admin);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, Admin admin)
        {
            Admin existingAdmin = await _dbContext.Admins
            .SingleOrDefaultAsync(x => x.Id == id);

            if (existingAdmin != null)
            {
                existingAdmin.Username = admin.Username;
                existingAdmin.Email = admin.Email;
                existingAdmin.Password = admin.Password;
                existingAdmin.role = admin.role;

                int rowsAffected = await _dbContext.SaveChangesAsync();
                return rowsAffected > 0;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Admin existingAdmin = await _dbContext.Admins
            .SingleOrDefaultAsync(x => x.Id == id);

            if (existingAdmin != null)
            {
                _dbContext.Admins.Remove(existingAdmin);
                int rowsAffected = await _dbContext.SaveChangesAsync();
                return rowsAffected > 0;
            }

            return false;
        }
    }
}
