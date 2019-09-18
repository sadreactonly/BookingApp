using BookingApp.Controllers;
using BookingApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace BookingApp.Tests
{
	[TestClass]
	public class TestPlacesController
	{
		[TestMethod]
		public void PostPlace_ShouldReturnSamePlace()
		{
			var controller = new PlacesController(new TestBookingAppContext());

			var item = GetDemoPlace();

			var result =
				controller.PostPlace(item) as CreatedAtRouteNegotiatedContentResult<Place>;

			Assert.IsNotNull(result);
			Assert.AreEqual(result.RouteName, "DefaultApi");
			Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
			Assert.AreEqual(result.Content.Name, item.Name);
		}

		[TestMethod]
		public void PutPlace_ShouldReturnStatusCode()
		{
			var controller = new PlacesController(new TestBookingAppContext());

			var item = GetDemoPlace();

			var result = controller.PutPlace(item.Id, item) as StatusCodeResult;
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
		}

		[TestMethod]
		public void PutPlace_ShouldFail_WhenDifferentID()
		{
			var controller = new PlacesController(new TestBookingAppContext());

			var badresult = controller.PutPlace(999, GetDemoPlace());
			Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
		}

		[TestMethod]
		public void GetPlace_ShouldReturnPlaceWithSameID()
		{
			var context = new TestBookingAppContext();
			context.Places.Add(GetDemoPlace());

			var controller = new PlacesController(context);
			var result = controller.GetPlace(3) as OkNegotiatedContentResult<Place>;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Content.Id);
		}

		[TestMethod]
		public void GetPlaces_ShouldReturnAllPlaces()
		{
			var context = new TestBookingAppContext();
			context.Places.Add(new Place { Id = 1, Name = "Novi Sad", Region = GetDemoRegion(), Accomodations = new List<Accommodation>() });
			context.Places.Add(new Place { Id = 2, Name = "Loznica", Region = GetDemoRegion(), Accomodations = new List<Accommodation>() });
			context.Places.Add(new Place { Id = 3, Name = "Beograd", Region = GetDemoRegion(), Accomodations = new List<Accommodation>() });

			var controller = new PlacesController(context);
			var result = controller.GetPlaces() as TestPlaceDbSet;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Local.Count);
		}

		[TestMethod]
		public void DeletePlace_ShouldReturnOK()
		{
			var context = new TestBookingAppContext();
			var item = GetDemoPlace();
			context.Places.Add(item);

			var controller = new PlacesController(context);
			var result = controller.DeletePlace(3) as OkNegotiatedContentResult<Place>;

			Assert.IsNotNull(result);
			Assert.AreEqual(item.Id, result.Content.Id);
		}

		private Place GetDemoPlace()
		{
			return new Place() { Id = 3, Name = "Demo name", Region = GetDemoRegion(), Accomodations = new List<Accommodation>() };
		}

		private Region GetDemoRegion()
		{
			return new Region() { Id = 3, Name = "Demo name", Country = GetDemoCountry(), Places = new List<Place>() };
		}

		private Country GetDemoCountry()
		{
			return new Country() { Id = 3, Name = "Demo name", Code = "Demo code", Regions = new List<Region>() };
		}
	}
}