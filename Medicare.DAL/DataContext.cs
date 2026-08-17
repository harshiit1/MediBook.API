using Medicare.Domain.Aggregate.AuthUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Medicare.DAL
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserAggregate> SignUp { get; set; }
        public DbSet<AuthInfo> AuthUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
