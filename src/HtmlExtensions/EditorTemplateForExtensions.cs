using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Class to EditorTemplateFor
    /// </summary>
    public static class EditorTemplateForExtensions
    {
        /// <summary>
        /// Generate a input according with var type using the template mask of AngularJS
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Lambda of model</param>
        /// <param name="htmlAttributes">HTML Atributes to add on input</param>
        /// <param name="numberReplace">Number of replacements that will occurred on index of the model</param>
        /// <returns></returns>
        public static MvcHtmlString EditorTemplateFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, int? numberReplace = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            Type fieldType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

            string type = "text";

            if (fieldType == typeof(byte)
                || fieldType == typeof(sbyte)
                || fieldType == typeof(int)
                || fieldType == typeof(uint)
                || fieldType == typeof(long)
                || fieldType == typeof(decimal)
                || fieldType == typeof(double)
                || fieldType == typeof(float))
            {
                type = "number";
            }

            var dict = new RouteValueDictionary(htmlAttributes);
            return TemplateForExtensions.TemplateFor(html, expression, dict, type, numberReplace);
        }
    }
}
