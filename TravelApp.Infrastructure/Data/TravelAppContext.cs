using Microsoft.EntityFrameworkCore;
using TravelApp.Domain.Entities;

namespace TravelApp.Infrastructure.Data
{
    public class TravelAppContext : DbContext
    {
        public TravelAppContext(DbContextOptions<TravelAppContext> options) : base(options)
        {

        }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
