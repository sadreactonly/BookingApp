using BookingApp.Models;
using BookingApp.Resources;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
    [RoutePrefix("api/Accommodations")]
    public class AccommodationsController : ApiController
	{
		private IBAContext db;

		public AccommodationsController()
		{
			db = new BAContext();
		}

		public AccommodationsController(IBAContext context)
		{
			db = context;
		}

        /// <summary>
        /// Get all accommodations.
        /// </summary>
        /// <returns></returns>
        [Route(Routes.GET_ACCOMMODATIONS)]
        public IQueryable<Accommodation> GetAccommodations()
		{
			return db.Accommodations.Include(x => x.AccommodationType).Include(p => p.Place).Include(t => t.Rooms);
		}

        /// <summary>
        /// Get popular accommodations.
        /// Popular accommodations have average grade higher than 4.
        /// </summary>
        /// <returns></returns>
        [Route("GetPopularAccommodations")]
        public IQueryable<Accommodation> GetPopularAccommodations()
        {
            return db.Accommodations.Include(x => x.AccommodationType).Include(p => p.Place).Include(t => t.Rooms).Where(x => x.AverageGrade > 4);
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
			Accommodation accommodation = db.Accommodations.Find(id);
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

			db.MarkAsModified(accommodation);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AccommodationExists(id))
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

			if (AccommodationExists(accommodation.Id))
			{
				return BadRequest("Accommodation exists.");
			}


			accommodation.Place = db.Places.Find(accommodation.Place.Id);
			accommodation.AccommodationType = db.AccommodationTypes.Find(accommodation.AccommodationType.Id);
			accommodation.Comments = new System.Collections.Generic.List<Comment>();

			db.Accommodations.Add(accommodation);
			db.SaveChanges();

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
			Accommodation accommodation = db.Accommodations.Find(id);
			if (accommodation == null)
			{
				return NotFound();
			}

			db.Accommodations.Remove(accommodation);
			db.SaveChanges();

			return Ok(accommodation);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool AccommodationExists(int id)
		{
			return db.Accommodations.Count(e => e.Id == id) > 0;
		}
	}
}