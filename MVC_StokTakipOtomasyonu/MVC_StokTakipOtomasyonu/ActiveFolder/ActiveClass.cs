using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_StokTakipOtomasyonu.ActiveFolder
{
    public static class ActiveClass
    {
        public static string ActivePage(this HtmlHelper html, string control, string action)
        {
            string active = "";
            var routedata = html.ViewContext.RouteData;
            string routecontrol = (string)routedata.Values["controller"];
            string routeaction = (string)routedata.Values["action"];
            if (control == routecontrol && action == routeaction) active = "active";

            return active;
        }
    }
}