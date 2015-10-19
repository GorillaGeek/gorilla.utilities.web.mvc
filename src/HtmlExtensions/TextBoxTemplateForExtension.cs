using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class TextBoxTemplateForExtension
    {
        public static MvcHtmlString TextBoxTemplateFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          object htmlAttributes = null,
          string customIndex = null)
        {
            var result = htmlHelper.TextBoxFor(expression, htmlAttributes);
            var stringResult = TemplateForExtensions.ReplaceNameAndId(result.ToString(), htmlHelper, expression, customIndex);

            return MvcHtmlString.Create(stringResult);
        }

    }
}
