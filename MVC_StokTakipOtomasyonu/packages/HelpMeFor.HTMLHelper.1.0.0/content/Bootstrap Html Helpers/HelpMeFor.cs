using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// This HTML Helper class will generate responsible HTML tags for general user metadata help. It'll get the values from the 'Description' metadata from DataAnnotations. Attention: this requires Bootstrap's popover Tooltip to be enabled to work properly.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString HelpMeFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var description = metadata.Description;

            // Chekc if there's any description available, if not, returns empty.
            if (!string.IsNullOrEmpty(description))
            {
                // Creates the tag.
                var tag = new TagBuilder("a");

                // Adds the help icon from the GlyphiconFor HTML Helper with a few custom CSS classes to make it responsible.
                tag.InnerHtml = GlyphiconFor("question-sign", false, "class=hidden-sm, hidden-xs");

                
                // Adds Bootstrap's responsible CSS properties to the tag.
                tag.AddCssClass("hidden-sm hidden-sx");

                // Get the tag Id from MetaData.
                tag.Attributes.Add("id", "help-" + metadata.PropertyName);

                // Adds the data attributes for popover effects.
                tag.Attributes.Add("data-toggle", "tooltip");
                tag.Attributes.Add("data-placement", "right");
                tag.Attributes.Add("data-content", description);
                tag.Attributes.Add("data-container", "body");
                tag.Attributes.Add("title", description);

                // Set the link to # so the user can click on it and nothing wil happen.
                tag.Attributes.Add("href", "#");

                // Custom Style for better renderization.
                tag.Attributes.Add("style", "color: #474949; display: -webkit-inline-box !important; text-decoration: none !important;");

                // This will fix z fighting with the responsiveTag.
                tag.Attributes.Add("tabindex", "-1");

                // Starts JQuery on MouseOver.
                tag.Attributes.Add("onmouseover", "$('#help-" + metadata.PropertyName + "').tooltip('show')");

                // Creates the responsive tag for smaller devices.
                var responsiveTag = new TagBuilder("div") { InnerHtml = "<sup>" + description + "</sup>" };

                // Hides the responsive tag on bigger devices.
                responsiveTag.AddCssClass("hidden-lg hidden-md text-muted");

                // Returns both tags.
                return new MvcHtmlString(tag.ToString() + responsiveTag.ToString());
            }

            // Returns empty if no description available.
            return MvcHtmlString.Empty;
        }
    }
}