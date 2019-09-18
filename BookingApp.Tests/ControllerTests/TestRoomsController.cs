using BookingApp.Controllers;
using BookingApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using System;

namespace BookingApp.Tests
{
	[TestClass]
	public class TestRoomsController
	{
		[TestMethod]
		public void PostRoom_ShouldReturnSameRoom()
		{
			var controller = new RoomsController(new TestBookingAppContext());

			var item = GetDemoRoom();

			var result =
				controller.PostRoom(item) as CreatedAtRouteNegotiatedContentResult<Room>;

			Assert.IsNotNull(result);
			Assert.AreEqual(result.RouteName, "DefaultApi");
			Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
			Assert.AreEqual(result.Content.RoomNumber, item.RoomNumber);
		}

		[TestMethod]
		public void PutRoom_ShouldReturnStatusCode()
		{
			var controller = new RoomsController(new TestBookingAppContext());

			var item = GetDemoRoom();

			var result = controller.PutRoom(item.Id, item) as StatusCodeResult;
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
		}

		[TestMethod]
		public void PutRoom_ShouldFail_WhenDifferentID()
		{
			var controller = new RoomsController(new TestBookingAppContext());

			var badresult = controller.PutRoom(999, GetDemoRoom());
			Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
		}

		[TestMethod]
		public void GetRoom_ShouldReturnRoomWithSameID()
		{
			var context = new TestBookingAppContext();
			context.Rooms.Add(GetDemoRoom());

			var controller = new RoomsController(context);
			var result = controller.GetRoom(3) as OkNegotiatedContentResult<Room>;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Content.Id);
		}

		[TestMethod]
		public void GetRooms_ShouldReturnAllRooms()
		{
			var context = new TestBookingAppContext();
			context.Rooms.Add(new Room { Id = 1, Accomodation = GetDemoAccommodation(), BedCount = 5, Description = "Opis prve sobe", PricePerNight = 100, RoomNumber = 1, RoomReservations = new List<RoomReservation>()});
			context.Rooms.Add(new Room { Id = 2, Accomodation = GetDemoAccommodation(), BedCount = 5, Description = "Opis druge sobe", PricePerNight = 200, RoomNumber = 2, RoomReservations = new List<RoomReservation>() });
			context.Rooms.Add(new Room { Id = 3, Accomodation = GetDemoAccommodation(), BedCount = 5, Description = "Opis trece sobe", PricePerNight = 300, RoomNumber = 3, RoomReservations = new List<RoomReservation>() });

			var controller = new RoomsController(context);
			var result = controller.GetRooms() as TestRoomDbSet;

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Local.Count);
		}



		[TestMethod]
		public void DeleteRoom_ShouldReturnOK()
		{
			var context = new TestBookingAppContext();
			var item = GetDemoRoom();
			context.Rooms.Add(item);

			var controller = new RoomsController(context);
			var result = controller.DeleteRoom(3) as OkNegotiatedContentResult<Room>;

			Assert.IsNotNull(result);
			Assert.AreEqual(item.Id, result.Content.Id);
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