using BookingApp.Models;
using BookingApp.Services.Interfaces;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class CountriesController : ApiController
	{
		ICountryService countryService;

		public CountriesController(ICountryService countryService)
		{
			this.countryService = countryService;
		}
		// GET: api/Countries
		public IQueryable<Country> GetCountries()
		{
			return (IQueryable<Country>)countryService.GetAll();
		}

		// GET: api/Countries/5

		[ResponseType(typeof(Country))]
		public IHttpActionResult GetCountry(int id)
		{
			Country country = countryService.GetById(id);
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


			if (countryService.Update(id, country))
			{
				return NotFound();
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


			countryService.Add(country);

			return CreatedAtRoute("DefaultApi", new { id = country.Id }, country);
		}

		// DELETE: api/Countries/5
		[HttpDelete]
		//[ResponseType(typeof(Country))]
		public IHttpActionResult DeleteCountry([FromUri] int id)
		{
			Country country = countryService.GetById(id);
			if (country == null)
			{
				return NotFound();
			}

			countryService.Delete(country.Id);

			return Ok(country);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				countryService.Dispose();
			}
			base.Dispose(disposing);
		}


	}
}