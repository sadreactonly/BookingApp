using BookingApp.Models;
using BookingApp.Services.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class RoomsController : ApiController
	{
		private IRoomService roomService;

		public RoomsController(IRoomService roomService)
		{
			this.roomService = roomService;
		}

		// GET: api/Rooms
		public IQueryable<Room> GetRooms()
		{
			return (IQueryable<Room>)roomService.GetAll();
		}

		// GET: api/Rooms/5
		[ResponseType(typeof(Room))]
		public IHttpActionResult GetRoom(int id)
		{
			Room room = roomService.GetById(id);
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

			roomService.Update(id, room);

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

			roomService.Add(room);

			return CreatedAtRoute("DefaultApi", new { id = room.Id }, room);
		}

		// DELETE: api/Rooms/5
		[ResponseType(typeof(Room))]
		public IHttpActionResult DeleteRoom(int id)
		{
			Room room = roomService.GetById(id);
			if (room == null)
			{
				return NotFound();
			}

			roomService.Delete(id);

			return Ok(room);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				roomService.Dispose();
			}
			base.Dispose(disposing);
		}

		
	}
}