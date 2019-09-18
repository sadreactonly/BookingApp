using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingApp.Models
{
	public class Room
	{
		public int Id { get; set; }
		public int RoomNumber { get; set; }
		public int BedCount { get; set; }
		public string Description { get; set; }
		public double PricePerNight { get; set; }

		[JsonIgnore]
		public List<RoomReservation> RoomReservations { get; set; }

		public Accommodation Accomodation { get; set; }

		public Room()
		{
		}
	}
}