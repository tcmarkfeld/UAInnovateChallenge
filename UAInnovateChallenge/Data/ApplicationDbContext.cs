using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UAInnovateChallenge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UAInnovateChallenge.Models.Bar> Bar { get; set; }
        public DbSet<UAInnovateChallenge.Models.BarPosts> BarPosts { get; set; }
        public DbSet<UAInnovateChallenge.Models.Employee> Employee { get; set; }
        public DbSet<UAInnovateChallenge.Models.User> User { get; set; }
        public DbSet<UAInnovateChallenge.Models.UserFavorites> UserFavorites { get; set; }
        public DbSet<UAInnovateChallenge.Models.UserPosts> UserPosts { get; set; }
    }
}