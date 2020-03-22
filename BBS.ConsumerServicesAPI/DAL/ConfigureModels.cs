using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace BBS.ConsumerServicesAPI.DAL
{
	public class ConfigureModels
	{
		private static string schema = "bs";

		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Bus>().HasKey(x => x.Id);
			modelBuilder.Entity<Destination>().HasKey(x => x.Id);
			modelBuilder.Entity<Trip>().HasKey(x => x.Id);
			modelBuilder.Entity<Ticket>().HasKey(x => x.Id);

			modelBuilder.Entity<Bus>().ToTable("Buses", schema);

			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Trip)
				.WithMany(t => t.Tickets)
				.HasForeignKey(t => t.TripId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Trip>()
				.HasOne(t => t.Bus)
				.WithMany(b => b.Trips)
				.HasForeignKey(t => t.BusId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Trip>().Property(a => a.TicketPrice).HasColumnType("decimal(18, 2)");

			modelBuilder.Entity<Destination>()
				.HasMany(d => d.TripsFromHere)
				.WithOne(t => t.DestinationFrom)
				.HasForeignKey(t => t.DestinationFromId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Destination>()
				.HasMany(d => d.TripsToHere)
				.WithOne(t => t.DestinationTo)
				.HasForeignKey(t => t.DestinationToId)
				.OnDelete(DeleteBehavior.NoAction);

		}
	}
}
