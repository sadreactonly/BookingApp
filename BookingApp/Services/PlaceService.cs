using BookingApp.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;
using BookingApp.Services.Interfaces;
using System;

namespace BookingApp.Services
{
	public class PlaceService : IPlaceService
	{
		IBAContext context;

		public PlaceService()
		{
			this.context = BAContext.Instance;
		}

		public bool Add(Place product)
		{
			product.Region = context.Regions.Find(product.Region.Id);
			context.Places.Add(product);
			context.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			Place place = context.Places.Find(id);
			if (place == null)
			{
				return false;
			}

			context.Places.Remove(place);
			context.SaveChanges();
			return true;
		}

		public void Dispose()
		{
			//context.Dispose();
		}

		public IEnumerable<Place> GetAll()
		{
			return context.Places.Include(p => p.Region.Country);
		}

		public Place GetById(int id)
		{
			Place country = context.Places.Find(id);
			return country;
		}

		public bool Update(int id, Place country)
		{
			context.MarkAsModified(country);

			try
			{
				context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PlaceExists(id))
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
		private bool PlaceExists(int id)
		{
			return context.Places.Count(e => e.Id == id) > 0;
		}
	}
}