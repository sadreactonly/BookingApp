using System;
using System.Data.Entity;

namespace BookingApp.Models
{
	public interface IBAContext : IDisposable
	{
		DbSet<Accommodation> Accommodations { get; set; }
		DbSet<AccommodationType> AccommodationTypes { get; set; }
		DbSet<Comment> Comments { get; set; }
		DbSet<Country> Countries { get; set; }
		DbSet<Place> Places { get; set; }
		DbSet<Region> Regions { get; set; }
		DbSet<Room> Rooms { get; set; }
		DbSet<RoomReservation> RoomReservations { get; set; }

		int SaveChanges();

		void MarkAsModified(Object item);
	}
}