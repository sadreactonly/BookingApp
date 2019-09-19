using BookingApp.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class RoomReservationsController : ApiController
	{
		private IBAContext db;

		public RoomReservationsController()
		{
			db = new BAContext();
		}

		public RoomReservationsController(IBAContext context)
		{
			db = context;
		}

		// GET: api/RoomReservations
		public IQueryable<RoomReservation> GetRoomReservations()
		{
			return db.RoomReservations.Include(x => x.Room);
		}

		// GET: api/RoomReservations/5
		[ResponseType(typeof(RoomReservation))]
		public IHttpActionResult GetRoomReservation(int id)
		{
			RoomReservation roomReservation = db.RoomReservations.Find(id);
			if (roomReservation == null)
			{
				return NotFound();
			}

			return Ok(roomReservation);
		}

		// PUT: api/RoomReservations/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutRoomReservation(int id, RoomReservation roomReservation)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != roomReservation.Id)
			{
				return BadRequest();
			}

			db.MarkAsModified(roomReservation);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RoomReservationExists(id))
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

		// POST: api/RoomReservations
		[ResponseType(typeof(RoomReservation))]
		public IHttpActionResult PostRoomReservation(RoomReservation roomReservation)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.RoomReservations.Add(roomReservation);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = roomReservation.Id }, roomReservation);
		}

		// DELETE: api/RoomReservations/5
		[ResponseType(typeof(RoomReservation))]
		public IHttpActionResult DeleteRoomReservation(int id)
		{
			RoomReservation roomReservation = db.RoomReservations.Find(id);
			if (roomReservation == null)
			{
				return NotFound();
			}

			db.RoomReservations.Remove(roomReservation);
			db.SaveChanges();

			return Ok(roomReservation);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool RoomReservationExists(int id)
		{
			return db.RoomReservations.Count(e => e.Id == id) > 0;
		}
	}
}