using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Todo.Models;


namespace Todo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoModels> Todos { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
            ) => optionsBuilder.UseSqlite(connectionString: "Datasource=app.db; Cache=Shared;");

    }
}