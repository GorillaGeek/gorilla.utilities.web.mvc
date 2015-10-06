using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Class to CheckBoxTemplateFor
    /// </summary>
    public static class CheckBoxTemplateForExtensions
    {

        /// <summary>
        /// Generate a input checkbox using the template mask of AngularJS
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Lambda of model</param>
        /// <param name="value">Current value for input</param>
        /// <param name="htmlAttributes">HTML Atributes to add on input</param>
        /// <param name="numberReplace">Number of replacements that will occurred on index of the model</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxTemplateFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, int? numberReplace = null)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return TemplateForExtensions.TemplateFor(html, expression, dict, "checkbox", numberReplace, true.ToString());
        }

    }
}
