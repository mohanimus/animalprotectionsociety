using System.Data.Entity;

namespace APSData.Entities
{
    public class APSContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
    }
}