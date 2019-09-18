using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingApp.Models
{
	public class Place
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Region Region { get; set; }

		[JsonIgnore]
		public List<Accommodation> Accomodations { get; set; }

		public Place()
		{
		}
	}
}