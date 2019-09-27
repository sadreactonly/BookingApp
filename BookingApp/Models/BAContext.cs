using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace BookingApp.Models
{
	public class BAContext : IdentityDbContext<BAIdentityUser>, IBAContext
	{

		public BAContext() : base("name=BookingApp")

		{
		}

        public DbSet<Accommodation> Accommodations { get; set; }

		public DbSet<AccommodationType> AccommodationTypes { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Country> Countries { get; set; }

		public DbSet<Place> Places { get; set; }

		public DbSet<Region> Regions { get; set; }

		public DbSet<RoomReservation> RoomReservations { get; set; }

		public DbSet<Room> Rooms { get; set; }

		public static BAContext Create()
		{
			return new BAContext();
		}

		public void MarkAsModified(object item)
		{
			Entry(item).State = EntityState.Modified;
		}
	}
}