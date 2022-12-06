using Lab8.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab8.DAL
{
    public class TripContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TypeTransport> TypesTransport { get; set; }

        public TripContext(DbContextOptions<TripContext> options) : base(options) { }
    }
}
