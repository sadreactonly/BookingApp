using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingApp.Models
{
	public class Region
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[JsonIgnore]
		public List<Place> Places { get; set; }

		public Country Country { get; set; }

		public Region()
		{
		}
	}
}