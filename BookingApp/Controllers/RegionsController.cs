using BookingApp.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class RegionsController : ApiController
	{
		private IBAContext db;

		public RegionsController()
		{
			db = new BAContext();
		}

		public RegionsController(IBAContext context)
		{
			db = context;
		}

		// GET: api/Regions
		public IQueryable<Region> GetRegions()
		{
			return db.Regions;
		}

		// GET: api/Regions/5
		[ResponseType(typeof(Region))]
		public IHttpActionResult GetRegion(int id)
		{
			Region region = db.Regions.Find(id);
			if (region == null)
			{
				return NotFound();
			}

			return Ok(region);
		}

		// PUT: api/Regions/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutRegion(int id, Region region)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != region.Id)
			{
				return BadRequest();
			}

			db.MarkAsModified(region);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RegionExists(id))
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

		// POST: api/Regions
		[ResponseType(typeof(Region))]
		public IHttpActionResult PostRegion(Region region)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Regions.Add(region);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = region.Id }, region);
		}

		// DELETE: api/Regions/5
		[ResponseType(typeof(Region))]
		public IHttpActionResult DeleteRegion(int id)
		{
			Region region = db.Regions.Find(id);
			if (region == null)
			{
				return NotFound();
			}

			db.Regions.Remove(region);
			db.SaveChanges();

			return Ok(region);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool RegionExists(int id)
		{
			return db.Regions.Count(e => e.Id == id) > 0;
		}
	}
}