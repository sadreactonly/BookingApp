using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using BookingApp.Models;

[assembly: OwinStartup(typeof(BookingApp.Startup))]

namespace BookingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
         

        }

    }
}
