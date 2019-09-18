using System;

namespace BookingApp.Models
{
	public class RoomReservation
	{
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime Timestamp { get; set; }
		public Room Room { get; set; }
		public BAIdentityUser User { get; set; }

		public RoomReservation()
		{
		}
	}
}