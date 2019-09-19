using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace BookingApp
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

			ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<Accommodation>("Accommodations");
			builder.EntitySet<AccommodationType>("AccommodationTypes");
			builder.EntitySet<Comment>("Comments");
			builder.EntitySet<BAIdentityUser>("Users");
			builder.EntitySet<Place>("Places");
			builder.EntitySet<Room>("Rooms");
			config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
		}
	}
}
