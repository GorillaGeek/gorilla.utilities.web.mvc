using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Create a input for Angular ngRepeat
    /// </summary>
    public static class ValidationMessageTemplateForExtensions
    {
        /// <summary>
        /// Create a span for jquery validation
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Lambda for model</param>
        /// <param name="htmlAttributes">Additional html attrs</param>
        /// <param name="numberReplace">number of replacements</param>
        /// <returns></returns>
        public static MvcHtmlString ValidationMessageTemplateFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes = null, int? numberReplace = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string fullName = TemplateForExtensions.GenerateName(htmlHelper, expression, numberReplace);

            TagBuilder tagBuilder = new TagBuilder("span");
            tagBuilder.AddCssClass("field-validation-valid");
            tagBuilder.MergeAttribute("data-valmsg-replace", "true");
            tagBuilder.MergeAttribute("data-valmsg-for", fullName);

            if (htmlAttributes != null)
            {
                foreach (var attrs in htmlAttributes)
                {
                    tagBuilder.MergeAttribute(attrs.Key.Replace("_", "-"), attrs.Value.ToString());
                }
            }

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}