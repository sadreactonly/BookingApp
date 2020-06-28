using BookingApp.Models;
using BookingApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BookingApp.Services
{
	public class CountryService : ICountryService
	{
		private IBAContext db;

		public CountryService()
		{
			db = BAContext.Instance;
		}

		public bool Add(Country product)
		{
			db.Countries.Add(product);
			db.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			Country country = db.Countries.Find(id);
			if (country == null)
			{
				return false;
			}

			db.Countries.Remove(country);
			db.SaveChanges();
			return true;
		}

		public void Dispose()
		{
			//db.Dispose();
		}

		public IEnumerable<Country> GetAll()
		{
			return db.Countries;
		}

		public Country GetById(int id)
		{
			Country country = db.Countries.Find(id);
			return country;
		}

		public bool Update(int id, Country country)
		{
			db.MarkAsModified(country);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CountryExists(id))
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
		private bool CountryExists(int id)
		{
			return db.Countries.Count(e => e.Id == id) > 0;
		}
	}
}