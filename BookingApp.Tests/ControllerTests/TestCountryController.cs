using BookingApp.Controllers;
using BookingApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace BookingApp.Tests
{
	[TestClass]
	public class TestCountriesController
	{
		[TestMethod]
		public void PostCountry_ShouldReturnSameCountry()
		{
			var controller = new CountriesController(new TestBookingAppContext());

			var item = GetDemoCountry();

			var result =
				controller.PostCountry(item) as CreatedAtRouteNegotiatedContentResult<Country>;

			Assert.IsNotNull(result);
			Assert.AreEqual(result.RouteName, "DefaultApi");
			Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
			Assert.AreEqual(result.Content.Name, item.Name);
		}

		[TestMethod]
		public void PutCountry_ShouldReturnStatusCode()
		{
			var controller = new CountriesController(new TestBookingAppContext());

			var item = GetDemoCountry();

			var result = controller.PutCountry(item.Id, item) as StatusCodeResult;
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
		}

		[TestMethod]
		public void PutCountry_ShouldFail_WhenDifferentID()
		{
			var controller = new CountriesController(new TestBookingAppContext());

			var badresult = controller.PutCountry(999, GetDemoCountry());
			Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
		}

		[TestMethod]
		public void GetCountry_ShouldReturnCountryWithSameID()
		{
			var context = new TestBookingAppContext();
			context.Countries.Add(GetDemoCountry());

			var controller = new CountriesController(context);
			var result = controller.GetCountry(3) as OkNegotiatedContentResult<Country>;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Content.Id);
		}

		[TestMethod]
		public void GetCountrys_ShouldReturnAllCountrys()
		{
			var context = new TestBookingAppContext();
			context.Countries.Add(new Country { Id = 1, Name = "Srbija", Code = "RS", Regions = new List<Region>() });
			context.Countries.Add(new Country { Id = 2, Name = "Kosovo", Code = "KOS", Regions = new List<Region>() });
			context.Countries.Add(new Country { Id = 3, Name = "Hrvatska", Code = "CRO", Regions = new List<Region>() });

			var controller = new CountriesController(context);
			var result = controller.GetCountries() as TestCountryDbSet;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Local.Count);
		}

		[TestMethod]
		public void DeleteCountry_ShouldReturnOK()
		{
			var context = new TestBookingAppContext();
			var item = GetDemoCountry();
			context.Countries.Add(item);

			var controller = new CountriesController(context);
			var result = controller.DeleteCountry(3) as OkNegotiatedContentResult<Country>;

			Assert.IsNotNull(result);
			Assert.AreEqual(item.Id, result.Content.Id);
		}

		private Country GetDemoCountry()
		{
			return new Country() { Id = 3, Name = "Demo name", Code = "Demo code", Regions = new List<Region>() };
		}
	}
}