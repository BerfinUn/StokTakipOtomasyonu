using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// HTML Helper for displaying the MetaData Description from DataAnnotations.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression)
        {
            // Gets the Description from the MetadaData.
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var description = metadata.Description;
            
            // If there's a Description, creats and returns the tag.
            if (!string.IsNullOrEmpty(description))
            {
                var tag = new TagBuilder("span") { InnerHtml = description };

                return new MvcHtmlString(tag.ToString());
            }
            
            // If there is no description, returns empty.
            return MvcHtmlString.Empty;
        }
    }
}