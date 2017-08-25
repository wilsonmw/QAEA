using Microsoft.EntityFrameworkCore;
 
namespace qaea.Models
{
    public class QaeaContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public QaeaContext(DbContextOptions<QaeaContext> options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Address> Addresses {get; set;}
        public DbSet<Child> Children {get; set;}
        public DbSet<Appointment> Appointments {get; set;}
        public DbSet<Children_has_Users> Children_has_Users {get; set;}
    }
}
