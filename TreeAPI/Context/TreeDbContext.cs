using Microsoft.EntityFrameworkCore;
using TreeAPI.Models;

namespace TreeAPI.Context
{
    public class TreeDbContext : DbContext
    {
        private string _connectionString = "Server=DESKTOP-A0EEVH8\\SQLEXPRESS;Database=TreeAPIDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        public DbSet<Node> Nodes { get; set; }

                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {

          }
    }
}