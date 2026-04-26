using Microsoft.EntityFrameworkCore;
using TravelExpenseTracker_API.Data.Entities;

namespace TravelExpenseTracker_API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripCategory> TripCategories { get; set; }
    public DbSet<TripExpense> TripExpenses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TripCategory>().HasData(TripCategory.GetSeedData());
        modelBuilder.Entity<ExpenseCategory>().HasData(ExpenseCategory.GetSeedData());
    }
}
