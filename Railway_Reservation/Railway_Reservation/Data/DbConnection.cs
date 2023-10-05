using Microsoft.EntityFrameworkCore;
using Railway_Reservation.Model;
using System.Collections.Generic;

namespace Railway_Reservation.Data
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options) : base(options) { }

        public DbSet<User> User2 { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Booking> Trip { get; set; }
    }
}
