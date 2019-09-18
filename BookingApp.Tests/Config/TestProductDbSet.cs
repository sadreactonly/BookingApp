using System;
using System.Linq;
using BookingApp.Models;

namespace BookingApp.Tests
{
	class TestCountryDbSet : TestDbSet<Country>
	{
		public override Country Find(params object[] keyValues)
		{
			return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
		}
	}

	class TestPlaceDbSet : TestDbSet<Place>
	{
		public override Place Find(params object[] keyValues)
		{
			return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
		}
	}

	class TestRegionDbSet : TestDbSet<Region>
	{
		public override Region Find(params object[] keyValues)
		{
			return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
		}
	}

	class TestRoomDbSet : TestDbSet<Room>
	{
		public override Room Find(params object[] keyValues)
		{
			return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
		}
	}

}