using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class CheckBoxTemplateForExtension
    {
        public static MvcHtmlString CheckBoxTemplateFor<TModel>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, bool>> expression,
           object htmlAttributes = null,
           string customIndex = null)
        {
            var result = htmlHelper.CheckBoxFor(expression, htmlAttributes);
            var stringResult = TemplateForExtensions.ReplaceNameAndId(result.ToString(), htmlHelper, expression, customIndex);

            return MvcHtmlString.Create(stringResult);
        }
    }
}
