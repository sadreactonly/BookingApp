using BookingApp.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class RoomsController : ApiController
	{
		private IBAContext db;

		public RoomsController()
		{
			db = new BAContext();
		}

		public RoomsController(IBAContext context)
		{
			db = context;
		}

		// GET: api/Rooms
		public IQueryable<Room> GetRooms()
		{
			return db.Rooms;
		}

		// GET: api/Rooms/5
		[ResponseType(typeof(Room))]
		public IHttpActionResult GetRoom(int id)
		{
			Room room = db.Rooms.Find(id);
			if (room == null)
			{
				return NotFound();
			}

			return Ok(room);
		}

		// PUT: api/Rooms/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutRoom(int id, Room room)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != room.Id)
			{
				return BadRequest();
			}

			db.MarkAsModified(room);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RoomExists(id))
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

		// POST: api/Rooms
		[ResponseType(typeof(Room))]
		public IHttpActionResult PostRoom(Room room)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Rooms.Add(room);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = room.Id }, room);
		}

		// DELETE: api/Rooms/5
		[ResponseType(typeof(Room))]
		public IHttpActionResult DeleteRoom(int id)
		{
			Room room = db.Rooms.Find(id);
			if (room == null)
			{
				return NotFound();
			}

			db.Rooms.Remove(room);
			db.SaveChanges();

			return Ok(room);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool RoomExists(int id)
		{
			return db.Rooms.Count(e => e.Id == id) > 0;
		}
	}
}