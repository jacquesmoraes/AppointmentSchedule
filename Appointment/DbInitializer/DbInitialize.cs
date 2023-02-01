using Appointment.DbInitializer;
using Appointment.Models;
using Appointment.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DbInitializer
{
    public class DbInitialize : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
            
        {
            try
            {
                if(_context.Database.GetPendingMigrations().Count() > 0)
                {
                     _context.Database.Migrate();
                }
            }
            catch (Exception e)
            {

            }

            if (_context.Roles.Any(x => x.Name == Utilities.Helper.Admin)) return;

           
                  _roleManager.CreateAsync(new IdentityRole(Helper.Admin)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(Helper.Doctor)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(Helper.Patient)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "elitelab@elitelab.com.br",
                Email = "elitelab@elitelab.com.br",
                EmailConfirmed = true,
                Name = "Jacques"
            }, "Jacques123@").GetAwaiter().GetResult();

            ApplicationUser user = _context.Users.FirstOrDefault(x => x.Email == "elitelab@elitelab.com.br");
            _userManager.AddToRoleAsync(user, Helper.Admin).GetAwaiter().GetResult();
        }
    }
}
