using BookingApp.Models;
using BookingApp.Resources;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using BookingApp.Services;
using System.Collections.Generic;

namespace BookingApp.Controllers
{
    [RoutePrefix("api/Accommodations")]
    public class AccommodationsController : ApiController
	{
		private IAccommodationService accommodationService;

		public AccommodationsController(IAccommodationService accommodationService)
		{
			this.accommodationService = accommodationService;
		}

        /// <summary>
        /// Get all accommodations.
        /// </summary>
        /// <returns></returns>
        [Route(Routes.GET_ACCOMMODATIONS)]
        public IEnumerable<Accommodation> GetAccommodations()
		{
			return accommodationService.GetAll();
		}

        /// <summary>
        /// Get popular accommodations.
        /// Popular accommodations have average grade higher than 4.
        /// </summary>
        /// <returns></returns>
        [Route("GetPopularAccommodations")]
        public IEnumerable<Accommodation> GetPopularAccommodations()
        {
			return accommodationService.GetPopularAccommodations();

		}

        /// <summary>
        /// Get accommodation by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Accommodation))]
        [Route("GetAccommodation")]
        public IHttpActionResult GetAccommodation(int id)
		{
			Accommodation accommodation = accommodationService.GetById(id);
			if (accommodation == null)
			{
				return NotFound();
			}

			return Ok(accommodation);
		}

		/// <summary>
        /// Update existing accommodation.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accommodation"></param>
        /// <returns></returns>
		[ResponseType(typeof(void))]
        [Route("PutAccommodation")]
        public IHttpActionResult PutAccommodation(int id, Accommodation accommodation)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != accommodation.Id)
			{
				return BadRequest();
			}


			if (accommodationService.Update(id, accommodation))
			{
				return NotFound();
			}

			return StatusCode(HttpStatusCode.NoContent);
		}
        
        /// <summary>
        /// Post new accommodation.
        /// </summary>
        /// <param name="accommodation"></param>
        /// <returns></returns>
		[ResponseType(typeof(Accommodation))]
        [Route("PostAccommodation")]
        public IHttpActionResult PostAccommodation(Accommodation accommodation)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!accommodationService.Add(accommodation))
			{
				return BadRequest();
			}

			return CreatedAtRoute("DefaultApi", new { id = accommodation.Id }, accommodation);
		}


        /// <summary>
        /// Delete accommodation.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[ResponseType(typeof(Accommodation))]
        [Route("DeleteAccommodation")]
        public IHttpActionResult DeleteAccommodation(int id)
		{
			if (!accommodationService.Delete(id))
			{
				return NotFound();
			}

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				accommodationService.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}