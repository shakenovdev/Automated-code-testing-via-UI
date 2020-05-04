using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ScenarioContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Variable> Variables { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<Argument> Arguments { get; set; }
        public DbSet<Assert> Asserts { get; set; }

        public ScenarioContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=scenario.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>()
                .HasMany(x => x.Children)
                .WithOne(x => x.ParentFolder)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
