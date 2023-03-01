using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using ProjectIVVer2.Data;
using ProjectIVVer2.Repository;
using ProjectIVVer2.Services;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ProjectIVVer2.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfigApplication(this IServiceCollection services, IConfiguration config)
        {
            //DBContext
            services.AddDbContext<EcommerceDBContext>(options => options.UseSqlServer(config.GetConnectionString("ConnectDB")));
            //Mapper
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Services
            //services.Configure<MailSettings>(config.GetSection("MailSettings"));
            //services.AddTransient<IEmailSender, SendMailService>();
            services.AddScoped<IAdmin, AdminService>();
            //services.AddScoped<IEmployeeService, EmployeeService>();
            //services.AddScoped<IWarehouseService, WarehouseService>();
            //services.AddScoped<IPositionService, PositionService>();
            //services.AddScoped<ICustomerService, CustomerService>();
            //services.AddScoped<IRentalPositionService, RentalPositionService>();
            //services.AddScoped<IPalletService, PalletService>();
            //services.AddScoped<IBookingService, BookingService>();
            //Config IndentityOptions
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!#";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });
            //services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationContext>();
            return services;
        }
    }
}
