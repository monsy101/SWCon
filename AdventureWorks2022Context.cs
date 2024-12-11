using Microsoft.EntityFrameworkCore;
using SWCon; // Ensure this matches the namespace of your Product class

public class AdventureWorks2022Context : DbContext
{
    public AdventureWorks2022Context(DbContextOptions<AdventureWorks2022Context> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    // Add other DbSets as needed
}