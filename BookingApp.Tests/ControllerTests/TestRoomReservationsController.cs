using BookingApp.Controllers;
using BookingApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace BookingApp.Tests
{
	[TestClass]
	public class TestRoomReservationReservationsController
	{
		[TestMethod]
		public void PostRoomReservation_ShouldReturnSameRoomReservation()
		{
			var controller = new RoomReservationsController(new TestBookingAppContext());

			var item = GetDemoRoomReservation();

			var result =
				controller.PostRoomReservation(item) as CreatedAtRouteNegotiatedContentResult<RoomReservation>;

			Assert.IsNotNull(result);
			Assert.AreEqual(result.RouteName, "DefaultApi");
			Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
			Assert.AreEqual(result.Content.Room, item.Room);
		}

		[TestMethod]
		public void PutRoomReservation_ShouldReturnStatusCode()
		{
			var controller = new RoomReservationsController(new TestBookingAppContext());

			var item = GetDemoRoomReservation();

			var result = controller.PutRoomReservation(item.Id, item) as StatusCodeResult;
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
		}

		[TestMethod]
		public void PutRoomReservation_ShouldFail_WhenDifferentID()
		{
			var controller = new RoomReservationsController(new TestBookingAppContext());

			var badresult = controller.PutRoomReservation(999, GetDemoRoomReservation());
			Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
		}

		[TestMethod]
		public void GetRoomReservation_ShouldReturnRoomReservationWithSameID()
		{
			var context = new TestBookingAppContext();
			context.RoomReservations.Add(GetDemoRoomReservation());

			var controller = new RoomReservationsController(context);
			var result = controller.GetRoomReservation(3) as OkNegotiatedContentResult<RoomReservation>;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Content.Id);
		}

		[TestMethod]
		public void GetRoomReservations_ShouldReturnAllRoomReservations()
		{
			var context = new TestBookingAppContext();
			context.RoomReservations.Add(new RoomReservation { Id = 1, StartDate = DateTime.Now, EndDate = DateTime.MaxValue, Timestamp = DateTime.Now, User = new BAIdentityUser(), Room = GetDemoRoom() });
			context.RoomReservations.Add(new RoomReservation { Id = 2, StartDate = DateTime.Now, EndDate = DateTime.MaxValue, Timestamp = DateTime.Now, User = new BAIdentityUser(), Room = GetDemoRoom() });
			context.RoomReservations.Add(new RoomReservation { Id = 3, StartDate = DateTime.Now, EndDate = DateTime.MaxValue, Timestamp = DateTime.Now, User = new BAIdentityUser(), Room = GetDemoRoom() });

			var controller = new RoomReservationsController(context);
			var result = controller.GetRoomReservations() as TestRoomReservationDbSet;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Local.Count);
		}

		[TestMethod]
		public void DeleteRoomReservation_ShouldReturnOK()
		{
			var context = new TestBookingAppContext();
			var item = GetDemoRoomReservation();
			context.RoomReservations.Add(item);

			var controller = new RoomReservationsController(context);
			var result = controller.DeleteRoomReservation(3) as OkNegotiatedContentResult<RoomReservation>;

			Assert.IsNotNull(result);
			Assert.AreEqual(item.Id, result.Content.Id);
		}

		private RoomReservation GetDemoRoomReservation()
		{
			return new RoomReservation { Id = 3, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue, Timestamp = DateTime.MaxValue, User = new BAIdentityUser(), Room = GetDemoRoom() };
		}

		private Room GetDemoRoom()
		{
			return new Room { Id = 3, Accomodation = GetDemoAccommodation(), BedCount = 5, Description = "DemoRoom", PricePerNight = 100, RoomNumber = 1, RoomReservations = new List<RoomReservation>() };
		}

		private Accommodation GetDemoAccommodation()
		{
			return new Accommodation();
		}
	}
}