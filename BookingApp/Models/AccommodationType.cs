using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingApp.Models
{
	public class AccommodationType
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[JsonIgnore]
		public List<Accommodation> Accommodations { get; set; }

		public AccommodationType()
		{
		}
	}
}