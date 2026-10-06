using Futsal_Management.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DAO.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Arena> Arenas { get; set; }
        public DbSet<BookingInfo> BookingInfos { get; set; }
    }
}