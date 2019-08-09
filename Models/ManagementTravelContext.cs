using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTravel.Models
{
    public class ManagementTravelContext : DbContext
    {
        public ManagementTravelContext(DbContextOptions<ManagementTravelContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
