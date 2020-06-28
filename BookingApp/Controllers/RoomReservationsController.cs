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
	public class RoomReservationsController : ApiController
	{
		private IRoomReservationService reservationService;

		public RoomReservationsController(IRoomReservationService reservationService)
		{
			this.reservationService = reservationService;
		}

		// GET: api/RoomReservations
		public IQueryable<RoomReservation> GetRoomReservations()
		{
			return (IQueryable<RoomReservation>)reservationService.GetAll();
		}

		// GET: api/RoomReservations/5
		[ResponseType(typeof(RoomReservation))]
		public IHttpActionResult GetRoomReservation(int id)
		{
			RoomReservation roomReservation = reservationService.GetById(id);
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

			reservationService.Update(id, roomReservation);

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

			reservationService.Add(roomReservation);

			return CreatedAtRoute("DefaultApi", new { id = roomReservation.Id }, roomReservation);
		}

		// DELETE: api/RoomReservations/5
		[ResponseType(typeof(RoomReservation))]
		public IHttpActionResult DeleteRoomReservation(int id)
		{
			RoomReservation roomReservation = reservationService.GetById(id);
			if (roomReservation == null)
			{
				return NotFound();
			}

			reservationService.Delete(id);

			return Ok(roomReservation);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				reservationService.Dispose();
			}
			base.Dispose(disposing);
		}

		
	}
}