using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.DAL
{
	public class DataSeeder
	{
		public static void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Bus>().HasData(new Bus { Id = 1, Model = "Vacanza 13", SeatsCount = 52 });

			modelBuilder.Entity<Destination>().HasData(
				new Destination { Id = 1, Name = "Tbilisi", Address = "Tbilisi bus station" },
				new Destination { Id = 2, Name = "Batumi", Address = "Batumi bus station" });

			modelBuilder.Entity<Trip>().HasData(
				new Trip { Id = 1, BusId = 1, Departure = new DateTime(2020, 2, 1, 11, 0, 0), FreeSeatCount = 52, DestinationFromId = 1, DestinationToId = 2, TicketPrice = 10, TravelTime = 7 },
				new Trip { Id = 2, BusId = 1, Departure = new DateTime(2020, 2, 2, 11, 0, 0), FreeSeatCount = 52, DestinationFromId = 1, DestinationToId = 2, TicketPrice = 10, TravelTime = 7 },
				new Trip { Id = 3, BusId = 1, Departure = new DateTime(2020, 2, 3, 11, 0, 0), FreeSeatCount = 52, DestinationFromId = 1, DestinationToId = 2, TicketPrice = 10, TravelTime = 7 },
				new Trip { Id = 4, BusId = 1, Departure = new DateTime(2020, 2, 4, 11, 0, 0), FreeSeatCount = 52, DestinationFromId = 1, DestinationToId = 2, TicketPrice = 10, TravelTime = 7 },
				new Trip { Id = 5, BusId = 1, Departure = new DateTime(2020, 2, 5, 11, 0, 0), FreeSeatCount = 52, DestinationFromId = 1, DestinationToId = 2, TicketPrice = 10, TravelTime = 7 },
				new Trip { Id = 6, BusId = 1, Departure = new DateTime(2020, 2, 4, 16, 0, 0), FreeSeatCount = 52, DestinationFromId = 2, DestinationToId = 1, TicketPrice = 10, TravelTime = 440 },
				new Trip { Id = 7, BusId = 1, Departure = new DateTime(2020, 2, 5, 16, 0, 0), FreeSeatCount = 52, DestinationFromId = 2, DestinationToId = 1, TicketPrice = 10, TravelTime = 440 }
				);

			modelBuilder.Entity<Ticket>()
				.HasData(
					new Ticket { Id = 1, PassangerName = "Levan Jintcharadze", PurchaseDate = new DateTime(2020, 2, 5, 17, 0, 0), SeatNumber = 20, TripId = 7 },
					new Ticket { Id = 2, PassangerName = "Dimitri Dondoladze", PurchaseDate = new DateTime(2020, 2, 7, 19, 0, 0), SeatNumber = 14, TripId = 3 },
					new Ticket { Id = 3, PassangerName = "Zura Kavtaradze", PurchaseDate = new DateTime(2020, 2, 8, 21, 0, 0), SeatNumber = 33, TripId = 5 }
				);
		}
	}
}
