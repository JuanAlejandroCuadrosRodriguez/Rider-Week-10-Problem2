using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data.Context;

public class BooksDBContext : DbContext
{
    public BooksDBContext()
    {
        
    }
    public BooksDBContext(DbContextOptions<BooksDBContext> options) : base(options)
    {
        
    }
    public DbSet<Book> Books { get; set; } 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;pwd=Ju!081204;Database=Books",
                serverVersion);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Book>().ToTable("Book");
        
    }
}