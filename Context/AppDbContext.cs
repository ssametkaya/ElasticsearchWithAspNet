using Microsoft.EntityFrameworkCore;

namespace ElasticsearchWithAspNet.Context
{
    public sealed class AppDbContext:DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-94U96R1;Database=TestDb;Integrated Security=true;");
        }

        public DbSet<Travel> Travels { get; set; }
    }

    public sealed class Travel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
