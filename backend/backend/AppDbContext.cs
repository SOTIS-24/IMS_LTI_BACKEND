using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
    }
}
