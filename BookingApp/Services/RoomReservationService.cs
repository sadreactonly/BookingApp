using BookingApp.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;
using BookingApp.Services.Interfaces;
using System;

namespace BookingApp.Services
{
	public class RoomReservationService : IRoomReservationService
	{
		private IBAContext context;

		public RoomReservationService()
		{
			context = BAContext.Instance;
		}
		public bool Add(RoomReservation product)
		{
			product.Room = context.Rooms.Find(product.Room.Id);
			context.RoomReservations.Add(product);
			context.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			RoomReservation place = context.RoomReservations.Find(id);
			if (place == null)
			{
				return false;
			}

			context.RoomReservations.Remove(place);
			context.SaveChanges();
			return true;
		}

		public void Dispose()
		{
			//context.Dispose();
		}

		public IEnumerable<RoomReservation> GetAll()
		{
			return context.RoomReservations.Include(x => x.Room).Include(y=>y.Room.Accomodation)
				.Include(z=>z.Room.Accomodation.Place)
				.Include(z => z.Room.Accomodation.Place.Region)
				.Include(z => z.Room.Accomodation.Place.Region.Country);
		}

		public RoomReservation GetById(int id)
		{
			return GetAll().FirstOrDefault(x => x.Id == id);
		}

		public bool Update(int id, RoomReservation region)
		{
			context.MarkAsModified(region);

			try
			{
				context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RoomReservationExists(id))
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
		private bool RoomReservationExists(int id)
		{
			return context.RoomReservations.Count(e => e.Id == id) > 0;
		}
	}
}