using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class RadioButtonTemplateForExtension
    {
        public static MvcHtmlString RadioButtonTemplateFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object value,
            object htmlAttributes = null,
            string customIndex = null)
        {
            var result = htmlHelper.RadioButtonFor(expression, value, htmlAttributes);
            var stringResult = TemplateForExtensions.ReplaceNameAndId(result.ToString(), htmlHelper, expression, customIndex);

            return MvcHtmlString.Create(stringResult);
        }
    }
}
