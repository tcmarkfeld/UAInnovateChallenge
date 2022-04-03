using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UAInnovateChallenge.Data.Migrations;

namespace UAInnovateChallenge.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0 )
                {
                    _db.Database.Migrate();
                }
            }catch(Exception ex)
            {

            }

            if (_db.Roles.Any(r => r.Name == SD.Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Manager)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "timmarkfeld@gmail.com",
                Email = "timmarkfeld@gmail.com",
                EmailConfirmed = true

            }, "#RollAntelopes").GetAwaiter().GetResult();

            IdentityUser user = _db.Users.Where(u => u.Email == "timmarkfeld@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, SD.Admin).GetAwaiter().GetResult();
        }
    }
    }
