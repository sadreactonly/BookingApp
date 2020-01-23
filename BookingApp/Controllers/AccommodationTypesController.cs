using BookingApp.Models;
using BookingApp.Services;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class AccommodationTypesController : ApiController
	{
		private IAccommodationTypeService accommodationTypeService;

		public AccommodationTypesController(IAccommodationTypeService repository)
		{
			accommodationTypeService = repository;
		}

		[HttpGet]
		public IEnumerable<AccommodationType> GetAccommodationTypes()
		{
			return  accommodationTypeService.GetAll();
		}

		[ResponseType(typeof(AccommodationType))]
		public IHttpActionResult GetAccommodationType(int id)
		{
			AccommodationType accommodationType = accommodationTypeService.GetById(id);
			if (accommodationType == null)
			{
				return NotFound();
			}

			return Ok(accommodationType);
		}

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

			if (accommodationTypeService.Update(id,accommodationType))	
			{	
				return NotFound();	
			}
			
			return StatusCode(HttpStatusCode.NoContent);
		}

		[ResponseType(typeof(AccommodationType))]
		public IHttpActionResult PostAccommodationType(AccommodationType accommodationType)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!accommodationTypeService.Add(accommodationType))
			{
				return BadRequest();
			}

			return CreatedAtRoute("DefaultApi", new { id = accommodationType.Id }, accommodationType);
		}

		[ResponseType(typeof(AccommodationType))]
		public IHttpActionResult DeleteAccommodationType(int id)
		{
			if (!accommodationTypeService.Delete(id))
			{
				return NotFound();
			}

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				accommodationTypeService.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}