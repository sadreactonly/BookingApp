using BookingApp.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;
using BookingApp.Services.Interfaces;
using System;

namespace BookingApp.Services.Interfaces
{
	public class RegionService : IRegionService
	{
		IBAContext context;

		public RegionService()
		{
			this.context = BAContext.Instance;
		}

		public bool Add(Region product)
		{
			product.Country = context.Countries.Find(product.Country.Id);
			context.Regions.Add(product);
			context.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			Region place = context.Regions.Find(id);
			if (place == null)
			{
				return false;
			}

			context.Regions.Remove(place);
			context.SaveChanges();
			return true;
		}

		public void Dispose()
		{
			//context.Dispose();
		}

		public IEnumerable<Region> GetAll()
		{
			return context.Regions.Include(p => p.Country);
		}

		public Region GetById(int id)
		{
			Region region = GetAll().FirstOrDefault(p => p.Id == id); 
			return region;
		}

		public bool Update(int id, Region region)
		{
			context.MarkAsModified(region);

			try
			{
				context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RegionExists(id))
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
		private bool RegionExists(int id)
		{
			return context.Regions.Count(e => e.Id == id) > 0;
		}
	}
}