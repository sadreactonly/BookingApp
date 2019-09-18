using BookingApp.Controllers;
using BookingApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace BookingApp.Tests
{
	[TestClass]
	public class TestRegionsController
	{
		[TestMethod]
		public void PostRegion_ShouldReturnSameRegion()
		{
			var controller = new RegionsController(new TestBookingAppContext());

			var item = GetDemoRegion();

			var result =
				controller.PostRegion(item) as CreatedAtRouteNegotiatedContentResult<Region>;

			Assert.IsNotNull(result);
			Assert.AreEqual(result.RouteName, "DefaultApi");
			Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
			Assert.AreEqual(result.Content.Name, item.Name);
		}

		[TestMethod]
		public void PutRegion_ShouldReturnStatusCode()
		{
			var controller = new RegionsController(new TestBookingAppContext());

			var item = GetDemoRegion();

			var result = controller.PutRegion(item.Id, item) as StatusCodeResult;
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
		}

		[TestMethod]
		public void PutRegion_ShouldFail_WhenDifferentID()
		{
			var controller = new RegionsController(new TestBookingAppContext());

			var badresult = controller.PutRegion(999, GetDemoRegion());
			Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
		}

		[TestMethod]
		public void GetRegion_ShouldReturnRegionWithSameID()
		{
			var context = new TestBookingAppContext();
			context.Regions.Add(GetDemoRegion());

			var controller = new RegionsController(context);
			var result = controller.GetRegion(3) as OkNegotiatedContentResult<Region>;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Content.Id);
		}

		[TestMethod]
		public void GetRegions_ShouldReturnAllRegions()
		{
			var context = new TestBookingAppContext();
			context.Regions.Add(new Region { Id = 1, Name = "Vojvodina", Country = GetDemoCountry(), Places = new List<Place>() });
			context.Regions.Add(new Region { Id = 2, Name = "Macva", Country = GetDemoCountry(), Places = new List<Place>() });
			context.Regions.Add(new Region { Id = 3, Name = "Srem", Country = GetDemoCountry(), Places = new List<Place>() });

			var controller = new RegionsController(context);
			var result = controller.GetRegions() as TestRegionDbSet;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Local.Count);
		}

		[TestMethod]
		public void DeleteRegion_ShouldReturnOK()
		{
			var context = new TestBookingAppContext();
			var item = GetDemoRegion();
			context.Regions.Add(item);

			var controller = new RegionsController(context);
			var result = controller.DeleteRegion(3) as OkNegotiatedContentResult<Region>;

			Assert.IsNotNull(result);
			Assert.AreEqual(item.Id, result.Content.Id);
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