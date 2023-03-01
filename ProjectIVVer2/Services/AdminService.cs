using ProjectIVVer2.Model;
using System.Data;
using ProjectIVVer2.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectIVVer2.Services
{
    public class AdminService : Repository.IAdmin
    {

        private readonly EcommerceDBContext dbContext;

        public AdminService(EcommerceDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Admin> LoginAsync(string username, string password)
        {
            var admin = dbContext.Admins.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
            return admin;
        }

        public async Task<Admin> GetByIdAsync(int id)
        {
            var admin = await dbContext.Admins.FindAsync(id);
            return admin;
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            var admins = await dbContext.Admins.ToListAsync();
            return admins;
        }

        public async Task<bool> CreateAsync(Admin admin)
        {
            var model = dbContext.Admins.SingleOrDefault(a => a.Id.Equals(admin.Id));
            if(model == null)
            {
                await dbContext.Admins.AddAsync(admin);
                await   dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAdmin(string username)
        {
            try
            {
                var admin = await   dbContext.Admins.FirstOrDefaultAsync(x => x.Username == username);

                if (admin != null)
                {
                    dbContext.Admins.Remove(admin);
                    await dbContext.SaveChangesAsync();
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
            Admin existingAdmin = await     dbContext.Admins
            .SingleOrDefaultAsync(x => x.Id == id);

            if (existingAdmin != null)
            {
                existingAdmin.Username = admin.Username;
                existingAdmin.Email = admin.Email;
                existingAdmin.Password = admin.Password;
                existingAdmin.role = admin.role;

                int rowsAffected = await dbContext.SaveChangesAsync();
                return rowsAffected > 0;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {   
            Admin existingAdmin = await dbContext.Admins
            .SingleOrDefaultAsync(x => x.Id == id);

            if (existingAdmin != null)
            {
                dbContext.Admins.Remove(existingAdmin);
                int rowsAffected = await dbContext.SaveChangesAsync();
                return rowsAffected > 0;
            }

            return false;
        }
    }
}
