using System.Linq.Expressions;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Class to TextBoxTemplateFor
    /// </summary>
    public static class TextAreaTemplateForExtensions
    {
        /// <summary>
        /// Generate a input text using the template mask of AngularJS
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Lambda of model</param>
        /// <param name="htmlAttributes">HTML Atributes to add on input</param>
        /// <param name="numberReplace">Number of replacements that will occurred on index of the model</param>
        /// <returns></returns>
        public static MvcHtmlString TextAreaTemplateFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null,
            int? numberReplace = null)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return TemplateForExtensions.TemplateFor(html, expression, dict, "textarea", numberReplace);
        }
    }
}
