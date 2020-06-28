using BookingApp.Models;
using BookingApp.Services.Interfaces;
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
		private IPlaceService placeService;
		public PlacesController(IPlaceService placeService)
		{
			this.placeService = placeService;
		}

		// GET: api/Places
		public IQueryable<Place> GetPlaces()
		{

			return (IQueryable<Place>)placeService.GetAll();
		}

		// GET: api/Places/5
		[ResponseType(typeof(Place))]
		public IHttpActionResult GetPlace(int id)
		{
			Place place = placeService.GetById(id);
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

			placeService.Update(id,place);

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

			try
			{
				placeService.Add(place);
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
			Place place = placeService.GetById(id);
			if (place == null)
			{
				return NotFound();
			}

			placeService.Delete(id);
			return Ok(place);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				placeService.Dispose();
			}
			base.Dispose(disposing);
		}


	}
}