using Microsoft.EntityFrameworkCore;

namespace Bug_Tracking_System.Models
{
    public class BugsTrackingContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Bug> Bug { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public BugsTrackingContext(DbContextOptions<BugsTrackingContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure if no options provided (for migrations/design-time)
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer
                    ("Data Source=EZZ-PC\\SQLEXPRESS;Initial Catalog=BugsTrackingDb;Integrated Security=True;Encrypt=False;Trust Server Certificate=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
