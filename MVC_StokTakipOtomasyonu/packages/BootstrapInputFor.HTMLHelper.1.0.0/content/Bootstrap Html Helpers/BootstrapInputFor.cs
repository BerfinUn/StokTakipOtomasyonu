using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// This HTML Helper will add a Bootstrap stylized input.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="expression"></param>
        /// <param name="events">Custom properties to be added to the tag. Separate them using the equals (=) sign.</param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapInputFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression, [Optional] params string[] events)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);

            // Creates the input tag.
            var input = new TagBuilder("input");

            // Replaces the value if the Model is not null.
            input.Attributes.Add("value", metadata.Model == null ? "" : metadata.Model.ToString());

            // General properties.
            input.Attributes.Add("id", metadata.PropertyName);
            input.Attributes.Add("name", metadata.PropertyName);

            // Stylize with Bootstrap.
            input.AddCssClass("form-control");
            input.Attributes.Add("type", "text");

            // Adds the custom attributes.
            Helpers.AddAtrributes(input, events);

            // Adds the validation properties.
            Helpers.AddValidationProperties(self, expression, input);

            // Returns the input.
            return new MvcHtmlString(input.ToString());
        }
    }
}