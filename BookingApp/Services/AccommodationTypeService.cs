using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BookingApp.Services
{
	public class AccommodationTypeService : IAccommodationTypeService
	{
		private IBAContext db;

		public AccommodationTypeService()
		{
			db = new BAContext();
		}
		public bool Add(AccommodationType accommodationType)
		{
			db.AccommodationTypes.Add(accommodationType);
			db.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			AccommodationType accommodationType = db.AccommodationTypes.Find(id);
			if (accommodationType == null)
			{
				return false;
			}

			db.AccommodationTypes.Remove(accommodationType);
			db.SaveChanges();
			return true;

		}

		public void Dispose()
		{
			db.Dispose();
		}

		public IEnumerable<AccommodationType> GetAll()
		{
			return db.AccommodationTypes;
		}

		public AccommodationType GetById(int id)
		{
			AccommodationType accommodationType = db.AccommodationTypes.Find(id);
			return accommodationType;
		}

		public bool Update(int id, AccommodationType accommodationType)
		{
			db.MarkAsModified(accommodationType);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AccommodationTypeExists(id))
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
		private bool AccommodationTypeExists(int id)
		{
			return db.AccommodationTypes.Count(e => e.Id == id) > 0;
		}
	}
}