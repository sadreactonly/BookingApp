using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Services.Interfaces;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace BookingApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			container.RegisterType<IAccommodationTypeService, AccommodationTypeService>();
			container.RegisterType<IAccommodationService, AccommodationService>();
            container.RegisterType<ICommentService, CommentService>();
            container.RegisterType<ICountryService, CountryService>();
            container.RegisterType<IPlaceService, PlaceService>();
            container.RegisterType<IRegionService, RegionService>();
            container.RegisterType<IRoomReservationService, RoomReservationService>();
            container.RegisterType<IRoomService, RoomService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}