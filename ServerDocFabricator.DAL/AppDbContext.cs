using Microsoft.EntityFrameworkCore;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.DAL.Entities.RefEntities;

namespace ColoredLive.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<TemplateFieldEntity> Fields { get; set; }
        public DbSet<TemplateEntity> Templates { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=base.db");
        }

    }
}
