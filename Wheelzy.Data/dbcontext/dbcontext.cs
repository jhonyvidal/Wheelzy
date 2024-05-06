
using Microsoft.EntityFrameworkCore;
using Wheelzy.Data.Entities;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Buyer> Buyer { get; set; }
    public DbSet<Quote> Quote { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<StatusHistory> StatusHistory { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> customer { get; set; }

}