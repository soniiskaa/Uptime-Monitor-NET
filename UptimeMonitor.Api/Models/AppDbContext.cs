using Microsoft.EntityFrameworkCore;

namespace UptimeMonitor.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MonitoredSite> Sites { get; set; }
    }
}
