using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class AppDbContext : DbContext
    {   
        
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Work" },
                new Category { Id = 2, Name = "Personal" },
                new Category { Id = 3, Name = "Shopping" }
                );
  
        }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskDetails> TaskDetails { get; set; }
        public DbSet<Category> Categories { get; set;  }
    }
}  
