using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace BBS.ConsumerServicesAPI.DAL
{
	public class ConsumerServiceDbContext : DbContext
	{
		public DbSet<Bus> Buses { get; set; }
		public DbSet<Destination> Destinations { get; set; }
		public DbSet<Trip> Trips { get; set; }
		public DbSet<Ticket> Tickets { get; set; }

		public ConsumerServiceDbContext(DbContextOptions<ConsumerServiceDbContext> options) : base(options)
		{
			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			ConfigureModels.Configure(modelBuilder);

			DataSeeder.Seed(modelBuilder);
		}
	}
}
