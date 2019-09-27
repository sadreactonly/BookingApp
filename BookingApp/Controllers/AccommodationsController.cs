using BookingApp.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
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

        // GET: api/Accommodations
        public IQueryable<Accommodation> GetAccommodations()
        {
            return db.Accommodations.Include(x => x.AccommodationType).Include(p => p.Place).Include(t => t.Rooms);
        }

        // GET: api/Accommodations/5
        [ResponseType(typeof(Accommodation))]
        public IHttpActionResult GetAccommodation(int id)
        {
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return Ok(accommodation);
        }

        // PUT: api/Accommodations/5
        [ResponseType(typeof(void))]
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

        // POST: api/Accommodations
        [ResponseType(typeof(Accommodation))]
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

        // DELETE: api/Accommodations/5
        [ResponseType(typeof(Accommodation))]
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