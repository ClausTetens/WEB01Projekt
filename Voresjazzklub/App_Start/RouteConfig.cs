using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Voresjazzklub {
    public class RouteConfig {

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            /*routes.MapRoute(
                name: "BilledeArrangs",
                url: "BilledeArrangs/{action}/{img}/{Upload}/{id}",
                defaults: new { controller = "BilledeArrangs", action = "Index", img="img", Upload="Upload", id = UrlParameter.Optional }
            );*/

            /*routes.MapRoute(
                name: "BilledeArrangs",
                url: "BilledeArrangs/{action}/{arran}/{id}",
                defaults: new { controller = "BilledeArrangs", action = "Index", arran=UrlParameter.Optional, id = UrlParameter.Optional }
            );*/
            routes.MapRoute(
                name: "BilledeArrangsEdit",
                url: "BilledeArrangs/Edit/{arran}/{id}",
                defaults: new { controller = "BilledeArrangs", action = "Edit", arran = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "BilledeArrangs",
                url: "BilledeArrangs/Details/{arran}/{id}",
                defaults: new { controller = "BilledeArrangs", action = "Details", arran = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "BilledeArrangsDelete",
                url: "BilledeArrangs/Delete/{arran}/{id}",
                defaults: new { controller = "BilledeArrangs", action = "Delete", arran = UrlParameter.Optional, id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Tilmeldte",
                url: "Tilmeldte/{arran}",
                defaults: new { controller = "Tilmeldingers", action = "Tilmeldte", arran = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "Brugere",
                url: "Brugere/{arran}",
                defaults: new { controller = "UsersTableModels", action = "Index", arran = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Bruger",
                url: "Bruger/{id}",
                defaults: new { controller = "UsersTableModels", action = "Details", id=UrlParameter.Optional  }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{bruger}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, bruger=UrlParameter.Optional}
            );
        }
    }
}
