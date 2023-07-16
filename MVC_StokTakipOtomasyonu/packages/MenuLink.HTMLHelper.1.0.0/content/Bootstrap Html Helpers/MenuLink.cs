namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// Creates 'li' HTML tags with contextual styling based on the actual request.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">Text to be displayed.</param>
        /// <param name="actionName">Action to point to.</param>
        /// <param name="controllerName">Controller to point to.</param>
        /// <returns>Returns a HTML 'li' string with the given context.</returns>
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            // Get the Action and the Controller for the actual request.
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            // Creates the tag.
            var li = new TagBuilder("li");

            // Creates a <a> inside the <li> tag, pointing to the given action and controller.
            li.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString();

            // Checks if the user's actual page is the point page at the <li>. If it is, put an 'active' css class to it for contextual displaying.
            if (controllerName == currentController && actionName == currentAction)
                li.AddCssClass("active");

            return new MvcHtmlString(li.ToString());
        }

        //Overload para a adicionar um ícone à esquerda.
        /// <summary>
        /// Creates 'li' HTML tags with contextual styling based on the actual request, with an glyphicon to the left.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">Text to be displayed.</param>
        /// <param name="actionName">Action to point to.</param>
        /// <param name="controllerName">Controller to point to.</param>
        /// <param name="iconClass">The glyphicon icon class. Can be without the 'glyphicon' and 'glyphicon-' values. Refeer to the actual version or Bootstrap in this project for the complete list of available Glyphicon at http://getbootstrap.com/components/#glyphicons</param>
        /// <returns>Returns a HTML 'li' string with the given context.</returns>
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string iconClass)
        {
            // Get the Action and the Controller for the actual request.
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            // Creates the icon 'span' tag.
            var icon = new TagBuilder("span");

            // Make sure it adds the icon if the user inserts all the glyphicon classes or only the name of the glyphicon.
            icon.AddCssClass("glyphicon glyphicon-" + iconClass.Replace("glyphicon glyphicon-", ""));

            // Creates the 'a' tag.
            var link = new TagBuilder("a")
            {
                InnerHtml = icon + "&nbsp;&nbsp;" + linkText // Adds two spaces between the icon and the text.
            };

            // Adds the link according to the given action and controllers parameters.
            link.Attributes.Add("href", "/" + controllerName + "/" + actionName);

            // Creates the 'li' tag.
            var li = new TagBuilder("li")
            {
                InnerHtml = link.ToString()
            };

            // Checks if the user's actual page is the point page at the <li>. If it is, put an 'active' css class to it for contextual displaying.
            if (controllerName == currentController && actionName == currentAction)
                li.AddCssClass("active");

            return new MvcHtmlString(li.ToString());
        }
    }
}