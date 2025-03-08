using Microsoft.EntityFrameworkCore;
using URLService.Entities;

namespace URLService.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<URL> URLs { get; set; }
        public DbSet<URLAnalytics> URLAnalytics { get; set; }
    }
}
