using System.Web.Http;
using WebActivatorEx;
using BookingApp;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace BookingApp
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c =>
              {
                  c.SingleApiVersion("v1", "BookingApp API");
                  c.IncludeXmlComments(string.Format(@"{0}\bin\BookingApp.xml",
                                       System.AppDomain.CurrentDomain.BaseDirectory));
              })
              .EnableSwaggerUi();
        }
    }
}
