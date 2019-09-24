using BookingApp.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class AccommodationTypesController : ApiController
	{
		private IBAContext db;

		public AccommodationTypesController()
		{
			db = new BAContext();
		}

		public AccommodationTypesController(IBAContext context)
		{
			db = context;
		}

		// GET: api/AccommodationTypes
		public IQueryable<AccommodationType> GetAccommodationTypes()
		{
			return db.AccommodationTypes;
		}

		// GET: api/AccommodationTypes/5
		[ResponseType(typeof(AccommodationType))]
		public IHttpActionResult GetAccommodationType(int id)
		{
			AccommodationType accommodationType = db.AccommodationTypes.Find(id);
			if (accommodationType == null)
			{
				return NotFound();
			}

			return Ok(accommodationType);
		}

		// PUT: api/AccommodationTypes/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutAccommodationType(int id, AccommodationType accommodationType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != accommodationType.Id)
			{
				return BadRequest();
			}

			db.MarkAsModified(accommodationType);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AccommodationTypeExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/AccommodationTypes
		[ResponseType(typeof(AccommodationType))]
		public IHttpActionResult PostAccommodationType(AccommodationType accommodationType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            if(AccommodationTypeExists(accommodationType.Id))
            {
                return BadRequest();
            }

			db.AccommodationTypes.Add(accommodationType);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = accommodationType.Id }, accommodationType);
		}

		// DELETE: api/AccommodationTypes/5
		[ResponseType(typeof(AccommodationType))]
		public IHttpActionResult DeleteAccommodationType(int id)
		{
			AccommodationType accommodationType = db.AccommodationTypes.Find(id);
			if (accommodationType == null)
			{
				return NotFound();
			}

			db.AccommodationTypes.Remove(accommodationType);
			db.SaveChanges();

			return Ok(accommodationType);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool AccommodationTypeExists(int id)
		{
			return db.AccommodationTypes.Count(e => e.Id == id) > 0;
		}
	}
}