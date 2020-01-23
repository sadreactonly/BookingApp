using BookingApp.Models;
using System.Data.Entity;

namespace BookingApp.Tests
{
	public class TestBookingAppContext : IBAContext
	{
		public TestBookingAppContext()
		{
			Countries = new TestCountryDbSet();
			Places = new TestPlaceDbSet();
			Regions = new TestRegionDbSet();
			Rooms = new TestRoomDbSet();
            RoomReservations = new TestRoomReservationDbSet();

        }

		public DbSet<Accommodation> Accommodations { get; set; }

		public DbSet<AccommodationType> AccommodationTypes { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Country> Countries { get; set; }

		public DbSet<Place> Places { get; set; }

		public DbSet<Region> Regions { get; set; }

		public DbSet<RoomReservation> RoomReservations { get; set; }

		public DbSet<Room> Rooms { get; set; }

		public int SaveChanges()
		{
			return 0;
		}

		public void MarkAsModified(object item)
		{
		}

		public void Dispose()
		{
		}
	}
}