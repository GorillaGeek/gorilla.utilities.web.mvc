using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class EnumDropDownListTemplateForExtension
    {
        public static MvcHtmlString EnumDropDownListTemplateFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            var result = htmlHelper.EnumDropDownListFor(expression, optionLabel, htmlAttributes);
            var stringResult = TemplateForExtensions.ReplaceNameAndId(result.ToString(), htmlHelper, expression, null);

            return MvcHtmlString.Create(stringResult);
        }
    }
}
