using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// This HTML Helper will add a Bootstrap stylized textarea.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="expression"></param>
        /// <param name="rows">The number of rows of the control. Default: 5.</param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression, [Optional, DefaultParameterValue(5)] int rows)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var description = metadata.Description;

            // Creates the textarea tag.
            var input = new TagBuilder("textarea");

            // Replaces the value if the Model is not null.
            input.Attributes.Add("value", metadata.Model == null ? "0" : metadata.Model.ToString().Remove(metadata.Model.ToString().Length - 2));

            // General properties.
            input.Attributes.Add("id", metadata.PropertyName);
            input.Attributes.Add("name", metadata.PropertyName);

            // Stylize with Bootstrap.
            input.AddCssClass("form-control");
            input.Attributes.Add("type", "text");

            // Add the number of rows.
            input.Attributes.Add("rows", rows.ToString());

            // Returns the textarea.
            return new MvcHtmlString(input.ToString());
        }
    }
}