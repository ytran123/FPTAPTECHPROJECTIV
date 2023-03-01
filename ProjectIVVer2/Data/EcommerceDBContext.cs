using Microsoft.EntityFrameworkCore;
using ProjectIVVer2.Model;
using System.Collections.Generic;

namespace ProjectIVVer2.Data
{
    public class EcommerceDBContext : DbContext
    {
        public EcommerceDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }
    }
}
