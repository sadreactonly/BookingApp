using BookingApp.Models;
using BookingApp.Services;
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

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}