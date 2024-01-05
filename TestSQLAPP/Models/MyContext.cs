using System;
using Microsoft.EntityFrameworkCore;

namespace TestSQLAPP.Models;

public class MyContext:DbContext
{
    public DbSet<Product> Products { get; set; }

    public MyContext(DbContextOptions<MyContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData
        (
            new Product{Id=1,Name="Pencil",Price=10.99},
            new Product{Id=2,Name="Pen",Price=5.99},
            new Product{Id=3,Name="Eraser",Price=3.99}
        );
    }
}