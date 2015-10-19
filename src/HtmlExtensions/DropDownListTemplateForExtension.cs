using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class DropDownListTemplateForExtension
    {
        public static MvcHtmlString DropDownListTemplateFor<TModel, TEnum>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TEnum>> expression,
           IEnumerable<SelectListItem> selectList,
           string optionLabel = null,
           object htmlAttributes = null,
           string customIndex = null)
        {
            var result = htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
            var stringResult = TemplateForExtensions.ReplaceNameAndId(result.ToString(), htmlHelper, expression, customIndex);

            return MvcHtmlString.Create(stringResult);
        }
    }
}
