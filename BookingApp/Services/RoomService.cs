using BookingApp.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;
using BookingApp.Services.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace BookingApp.Services
{
	public class RoomService : IRoomService
	{
		private IBAContext context;

		public RoomService()
		{
			context = BAContext.Instance;
		}
		public bool Add(Room product)
		{
			product.Accomodation = context.Accommodations.Find(product.Accomodation.Id);
			context.Rooms.Add(product);
			context.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			Room place = context.Rooms.Find(id);
			if (place == null)
			{
				return false;
			}

			context.Rooms.Remove(place);
			context.SaveChanges();
			return true;
		}

		public void Dispose()
		{
			//context.Dispose();
		}

		public IEnumerable<Room> GetAll()
		{
			return context.Rooms.Include(x => x.Accomodation)
				.Include(y => y.Accomodation.AccommodationType)
				.Include(y => y.Accomodation.Place)
				.Include(y => y.Accomodation.Place.Region)
				.Include(y => y.Accomodation.Place.Region.Country);
		}

		public Room GetById(int id)
		{
			return GetAll().FirstOrDefault(x => x.Id == id);
		}

		public bool Update(int id, Room region)
		{
			context.MarkAsModified(region);

			try
			{
				context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RoomExists(id))
				{
					return false;
				}
				else
				{
					throw;
				}
			}
			return true;
		}
		private bool RoomExists(int id)
		{
			return context.Rooms.Count(e => e.Id == id) > 0;
		}
	}
}