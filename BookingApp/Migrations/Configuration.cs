namespace BookingApp.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<BAContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(BAContext context)
		{
			if (!context.Roles.Any(r => r.Name == "Admin"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "Admin" };

				manager.Create(role);
			}

			if (!context.Roles.Any(r => r.Name == "Manager"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "Manager" };

				manager.Create(role);
			}

			if (!context.Roles.Any(r => r.Name == "AppUser"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "AppUser" };

				manager.Create(role);
			}

			var userStore = new UserStore<BAIdentityUser>(context);
			var userManager = new UserManager<BAIdentityUser>(userStore);

			if (!context.Users.Any(u => u.UserName == "admin"))
			{
				var user1 = new BAIdentityUser() { Id = "admin", UserName = "admin", Email = "admin@yahoo.com", PasswordHash = BAIdentityUser.HashPassword("admin") };
				userManager.Create(user1);
				userManager.AddToRole(user1.Id, "Admin");
			}

			BAIdentityUser user = new BAIdentityUser() { Id = "manager", UserName = "manager", Email = "manager@gmail.com", PasswordHash = BAIdentityUser.HashPassword("manager") };

			if (!context.Users.Any(u => u.UserName == "vasic"))
			{
				userManager.Create(user);
				userManager.AddToRole(user.Id, "Manager");
			}

			if (!context.Users.Any(u => u.UserName == "user"))
			{
				var user1 = new BAIdentityUser() { Id = "user", UserName = "user", Email = "user@gmail.com", PasswordHash = BAIdentityUser.HashPassword("user") };
				userManager.Create(user1);
				userManager.AddToRole(user1.Id, "AppUser");
			}

			user.Accomodations = new List<Accommodation>();
			user.Comments = new List<Comment>();
			user.RoomReservations = new List<RoomReservation>();

			context.SaveChanges();

			Country country = new Country();
			country.Id = 1;
			country.Name = "Serbia";
			country.Code = "RS";
			country.Regions = new List<Region>();

			Region region = new Region();
			region.Country = country;
			region.Id = 1;
			region.Name = "Vojvodina";
			region.Places = new List<Place>();
			country.Regions.Add(region);

			Place place = new Place();
			place.Id = 1;
			place.Name = "Novi Sad";
			place.Region = region;
			place.Accomodations = new List<Accommodation>();
			region.Places.Add(place);

			AccommodationType accType = new AccommodationType();
			accType.Id = 1;
			accType.Name = "acc Type";
			accType.Accommodations = new List<Accommodation>();

			Accommodation acc = new Accommodation();
			acc.Id = 1;
			acc.Address = "Zmaj Jovina 1";
			acc.Approved = false;
			acc.AverageGrade = 0;
			acc.Comments = new List<Comment>();
			acc.Description = "Opis Vile Rijane";
			acc.ImageURL = string.Empty;
			acc.Latitude = 0;
			acc.Longitude = 0;
			acc.Name = "Vila Rijana";
			acc.Owner = user;
			acc.Place = place;
			acc.Rooms = new List<Room>();
			accType.Accommodations.Add(acc);
			user.Accomodations.Add(acc);

			Room room = new Room();
			room.Accomodation = acc;
			room.BedCount = 3;
			room.Description = "Opis sobe.";
			room.Id = 1; ;
			room.PricePerNight = 100;
			room.RoomNumber = 1;
			room.RoomReservations = new List<RoomReservation>();
			acc.Rooms.Add(room);

			RoomReservation roomRes = new RoomReservation();
			roomRes.EndDate = DateTime.Now;
			roomRes.StartDate = DateTime.Now;
			roomRes.Timestamp = DateTime.Now;
			roomRes.User = user;
			roomRes.Room = room;
			roomRes.Id = 1;
			user.RoomReservations.Add(roomRes);

			Comment comm = new Comment();
			comm.Accomodation = acc;
			comm.Grade = 0;
			comm.Text = "Dobar komentar.";
			comm.User = user;
			comm.Id = 1;
			user.Comments.Add(comm);

			try
			{
				context.Accommodations.Add(acc);
				context.AccommodationTypes.Add(accType);
				context.Comments.Add(comm);
				context.Countries.Add(country);
				context.Places.Add(place);
				context.Regions.Add(region);
				context.RoomReservations.Add(roomRes);
				context.Rooms.Add(room);
				context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}