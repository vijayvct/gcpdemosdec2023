using Microsoft.EntityFrameworkCore;

namespace SQLMVCApp.Models;

public class MyContext:DbContext
{
    private readonly IConfiguration config;
    public MyContext(IConfiguration config)
    {
        this.config= config;
    }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(config.GetConnectionString("myconn"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData
        (
            new Product {Id=1,Name="Pen",Price=29.99},
            new Product {Id=2,Name="Notbook",Price=40},
            new Product{Id=3,Name="Pencil",Price=9.99}            
        );
    }
}