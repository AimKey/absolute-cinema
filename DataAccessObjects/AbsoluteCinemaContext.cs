using BusinessObjects.Models;
using DataAccessObjects.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessObjects
{
    public class AbsoluteCinemaContext : DbContext
    {
        public AbsoluteCinemaContext(DbContextOptions options) : base(options)
        {
        }

        public AbsoluteCinemaContext()
        {
        }
        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Absolute-cinema"))
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies() // Allow lazy loading like spring boot 
                .UseSqlServer(GetConnectionString());
        }

        // DbSet properties for each model
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors {  get; set; }
        public DbSet<MovieTag> MovieTags {  get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<ShowtimeSeat> ShowtimeSeats { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        // Configure the relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AbsoluteCinemaContext).Assembly);
            // modelBuilder.ApplyConfiguration(new UserDetailConfiguration());
            // modelBuilder.ApplyConfiguration(new UserConfiguration());
            // modelBuilder.ApplyConfiguration(new BookingConfiguration());
            // modelBuilder.ApplyConfiguration(new TicketConfiguration());
            // modelBuilder.ApplyConfiguration(new ShowtimeSeatConfiguration());
            // modelBuilder.ApplyConfiguration(new ShowtimeConfiguration());
            // modelBuilder.ApplyConfiguration(new SeatConfiguration());
            // modelBuilder.ApplyConfiguration(new RoomConfiguration());
        }

    }
}
