using BookingApp.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class CountriesController : ApiController
	{
		private IBAContext db;

		public CountriesController()
		{
			db = new BAContext();
		}

		public CountriesController(IBAContext context)
		{
			db = context;
		}

		// GET: api/Countries
		public IQueryable<Country> GetCountries()
		{
			return db.Countries;
		}

		// GET: api/Countries/5

		[ResponseType(typeof(Country))]
		public IHttpActionResult GetCountry(int id)
		{
			Country country = db.Countries.Find(id);
			if (country == null)
			{
				return NotFound();
			}

			return Ok(country);
		}

		// PUT: api/Countries/5
		
		[ResponseType(typeof(void))]
		public IHttpActionResult PutCountry(int id, Country country)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != country.Id)
			{
				return BadRequest();
			}

			db.MarkAsModified(country);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CountryExists(id))
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

		// POST: api/Countries
		[HttpPost]
		[ResponseType(typeof(Country))]
		public IHttpActionResult PostCountry([FromBody]Country country)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            if(CountryExists(country.Id))
            {
                return BadRequest();
            }

			db.Countries.Add(country);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = country.Id }, country);
		}

		// DELETE: api/Countries/5
		[HttpDelete]
		//[ResponseType(typeof(Country))]
		public IHttpActionResult DeleteCountry([FromUri] int id)
		{
			Country country = db.Countries.Find(id);
			if (country == null)
			{
				return NotFound();
			}

			db.Countries.Remove(country);
			db.SaveChanges();

			return Ok(country);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool CountryExists(int id)
		{
			return db.Countries.Count(e => e.Id == id) > 0;
		}
	}
}