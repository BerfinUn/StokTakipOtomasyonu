using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// This helper will create a pagination that will use Bootstrap 'ul' HTML tags and CSS classes, changing the last value in the url.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="CurrentPage">The current page being displayed.</param>
        /// <param name="TotalPages">The total number of pages to be displayed.</param>
        /// <param name="Itens">(Optional) The amount of itens to show per pagination row. Caution: exceeding the page length may cause render issues.</param>
        /// <returns>Returns the whole pagination html with links redirecting according to this: {currentUrl}/{pageNumber}</returns>
        public static MvcHtmlString PaginationFor(this HtmlHelper htmlHelper, int CurrentPage, int TotalPages, [Optional, DefaultParameterValue(10)] int Itens)
        {
            #region TagBuilders
            // Create the main tags.
            var ul = new TagBuilder("ul"); ul.AddCssClass("pagination");
            var a = new TagBuilder("a");
            var li = new TagBuilder("li");

            #endregion

            #region Lists and logic

            // Creates the lists that will receive the pages data.
            var LeftList = new List<int>();
            var RightList = new List<int>();
            var FullList = new List<int>();



            int Half = (int)Math.Round(new decimal(Itens / 2));

            for (int i = 1; i <= Itens; i++)
            {
                int CurrentAdd = ((CurrentPage - Half) + i) - 1;

                if (CurrentAdd <= 0)
                {
                    RightList.Add(CurrentAdd + Itens);
                }
                if (CurrentAdd > 0 && CurrentAdd < CurrentPage)
                {
                    LeftList.Add(CurrentAdd);
                }
                if (CurrentAdd > CurrentPage && CurrentAdd <= TotalPages)
                {
                    RightList.Add(CurrentAdd);
                }
                if (CurrentAdd > TotalPages)
                {
                    LeftList.Add(CurrentAdd - Itens);
                }
            }

            // Adds the pages to the left.
            FullList.AddRange(LeftList.OrderBy(p => p));

            // Adds the center page.
            FullList.Add(CurrentPage);

            // Adds the pages to te right.
            FullList.AddRange(RightList.OrderBy(p => p));

            #endregion

            #region Left arrow

            a = new TagBuilder("a");
            li = new TagBuilder("li");

            int PreviousPagination = CurrentPage - Itens;

            // Check if there is any page to the left.
            if (LeftList.Count == 0 || LeftList.OrderBy(l => l).FirstOrDefault() <= 0)
            {
                li.AddCssClass("disabled");
                li.InnerHtml = "<span>&laquo;</span>";
            }
            else
            {
                a.Attributes.Add("href", (PreviousPagination <= 0 ? 1 : PreviousPagination).ToString());
                a.InnerHtml = "&laquo;";
                li.InnerHtml = a.ToString();
            }

            // Finalize.
            ul.InnerHtml += li.ToString();

            #endregion

            #region Pages

            foreach (int idPage in FullList)
            {
                a = new TagBuilder("a");
                a.Attributes.Add("href", idPage.ToString());
                li = new TagBuilder("li");

                a.InnerHtml = idPage.ToString();

                // Put the 'active' css class if it is the current page being displayed.
                if (idPage == CurrentPage) li.AddCssClass("active");

                // Finalize.
                li.InnerHtml = a.ToString();
                ul.InnerHtml += li.ToString();
            }

            #endregion

            #region Right arrow

            a = new TagBuilder("a");
            li = new TagBuilder("li");

            int NextPagination = CurrentPage + Itens;

            // Check if there is any page to the right.
            if (RightList.Count == 0 || NextPagination >= TotalPages)
            { 
                li.AddCssClass("disabled");
                li.InnerHtml = "<span>&raquo;</span>";
            }
            else
            {
                a.Attributes.Add("href", (NextPagination > TotalPages ? TotalPages : NextPagination).ToString());
                a.InnerHtml = "&raquo;";
                li.InnerHtml = a.ToString();
            }

            // Finalize.
            ul.InnerHtml += li.ToString();

            #endregion

            return new MvcHtmlString(ul.ToString());
        }
    }
}