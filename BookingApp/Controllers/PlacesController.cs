using BookingApp.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class PlacesController : ApiController
	{
		private IBAContext db;

		public PlacesController()
		{
			db = new BAContext();
		}

		public PlacesController(IBAContext context)
		{
			db = context;
		}

		// GET: api/Places
		public IQueryable<Place> GetPlaces()
		{
			return db.Places.Include(p => p.Region);
		}

		// GET: api/Places/5
		[ResponseType(typeof(Place))]
		public IHttpActionResult GetPlace(int id)
		{
			Place place = db.Places.Find(id);
			if (place == null)
			{
				return NotFound();
			}

			return Ok(place);
		}

		// PUT: api/Places/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutPlace(int id, Place place)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != place.Id)
			{
				return BadRequest();
			}

			db.MarkAsModified(place);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PlaceExists(id))
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

		// POST: api/Places
		[ResponseType(typeof(Place))]
		public IHttpActionResult PostPlace(Place place)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			if (PlaceExists(place.Id))
			{
				return BadRequest();
			}
			try
			{
				place.Region = db.Regions.Find(place.Region.Id);
				db.Places.Add(place);
				db.SaveChanges();
				return CreatedAtRoute("DefaultApi", new { id = place.Id }, place);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return BadRequest();
			}


		}

		// DELETE: api/Places/5
		[ResponseType(typeof(Place))]
		public IHttpActionResult DeletePlace(int id)
		{
			Place place = db.Places.Find(id);
			if (place == null)
			{
				return NotFound();
			}

			db.Places.Remove(place);
			db.SaveChanges();

			return Ok(place);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool PlaceExists(int id)
		{
			return db.Places.Count(e => e.Id == id) > 0;
		}
	}
}