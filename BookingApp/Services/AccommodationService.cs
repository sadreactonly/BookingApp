using BookingApp.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;


namespace BookingApp.Services
{
	public class AccommodationService : IAccommodationService
	{
		private IBAContext db;

		public AccommodationService()
		{
			db = new BAContext();
		}
		public bool Add(Accommodation accommodation)
		{
			accommodation.Place = db.Places.Find(accommodation.Place.Id);
			accommodation.AccommodationType = db.AccommodationTypes.Find(accommodation.AccommodationType.Id);
			accommodation.Comments = new List<Comment>();
			db.Accommodations.Add(accommodation);
			db.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			Accommodation accommodation = db.Accommodations.Find(id);
			if (accommodation == null)
			{
				return false;
			}

			db.Accommodations.Remove(accommodation);
			db.SaveChanges();
			return true;

		}

		public IEnumerable<Accommodation> GetAll()
		{
			return db.Accommodations.Include(x => x.AccommodationType).Include(p => p.Place.Region.Country).Include(t => t.Rooms);
		}

		public IEnumerable<Accommodation> GetPopularAccommodations()
		{
			return db.Accommodations.Include(x => x.AccommodationType).Include(p => p.Place).Include(t => t.Rooms).Where(x => x.AverageGrade > 4);
		}

		public Accommodation GetById(int id)
		{
			Accommodation accommodation = db.Accommodations.Find(id);
			return accommodation;
		}

		public bool Update(int id, Accommodation accommodation)
		{
			db.MarkAsModified(accommodation);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AccommodationExists(id))
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
		private bool AccommodationExists(int id)
		{
			return db.Accommodations.Count(e => e.Id == id) > 0;
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}
}