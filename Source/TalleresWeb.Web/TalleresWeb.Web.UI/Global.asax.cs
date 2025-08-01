﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using TalleresWeb.Web.UI;

namespace TalleresWeb.Web.UI
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Código que se ejecuta al cerrarse la aplicación

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando se produce un error no controlado

        }
    }
}
