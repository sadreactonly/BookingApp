using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookingApp.Models
{
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }

		[JsonIgnore]
		public List<Region> Regions { get; set; }

		public Country()
		{
		}
	}
}