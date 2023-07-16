using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// Creates an input with a currency mask using JQuery, MoneyMask and Bootstrap. Remember to set the jquery.maskmoney.js reference to your page in order to use the currency mask.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="expression"></param>
        /// <param name="culture">The country culture for currency pattern. If informed, the helper will retrieve the culture data from the framework, if not, it'll retrieve from the Web.Config specified under the system.web/globalization property. If both are not informed, will get 'en-US' pattern as default. </param>
        /// <param name="events">Custom properties to be added to the tag. Separate them using the equals (=) sign.</param>
        /// <returns></returns>
        public static MvcHtmlString CurrencyFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression, [Optional] string culture, [Optional] params string[] events)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);

            // Creates the main Input tag and set general values.
            var input = new TagBuilder("input");
            input.Attributes.Add("id", metadata.PropertyName);
            input.Attributes.Add("name", metadata.PropertyName);
            input.Attributes.Add("type", "currency");

            // Bootstrap CSS class for inputs.
            input.AddCssClass("form-control");

            // Replaces the value if the Model is not null.
            input.Attributes.Add("value", metadata.Model == null ? "" : metadata.Model.ToString().Remove(metadata.Model.ToString().Length - 2));

            // Starts the JQuery.MoneyMask on element load.
            input.Attributes.Add("onfocus", "$('#" + metadata.PropertyName + "').maskMoney();");

            // Gets the Culture.
            CultureInfo currentCulture;

            try
            {
                currentCulture = new CultureInfo(culture, false);
            }
            catch (Exception)
            {
                // If culture is not found, returns the default below.
                currentCulture = new CultureInfo("en-US");
            }
            

            // Sets the placeholder format according to the culture.
            input.Attributes.Add("placeholder", "0" + currentCulture.NumberFormat.CurrencyDecimalSeparator + "00");


            // Currency mask data-* attributes according to the Culture.
            // Thousand separators.
            input.Attributes.Add("data-thousands", currentCulture.NumberFormat.CurrencyGroupSeparator);
            
            // Decimal separators.
            input.Attributes.Add("data-decimal", currentCulture.NumberFormat.CurrencyDecimalSeparator);
            
            // Adds the custom attributes.
            Helpers.AddAtrributes(input, events);


            // Prepend styling.
            // Addon (the icon container)
            var addon = new TagBuilder("span");
            addon.AddCssClass("input-group-addon");
            addon.InnerHtml = currentCulture.NumberFormat.CurrencySymbol;

            // Input group that will hold all the tags and define the prepend boundaries.
            var inputgroup = new TagBuilder("div");
            inputgroup.AddCssClass("input-group");

            // Adds the other elements inside the input group.
            inputgroup.InnerHtml = addon.ToString() + input.ToString();


            // Returns the input group.
            return new MvcHtmlString(inputgroup.ToString());
        }
    }
}