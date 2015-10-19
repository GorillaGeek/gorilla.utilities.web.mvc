using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace System.Web.Mvc.Html
{
    public static class ValidationMessageTemplateForExtensions
    {
        public static MvcHtmlString ValidationMessageTemplateFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string customIndex = null,
          string validationMessage = null,
          object htmlAttributes = null,
          string tag = "span")
        {
            var result = htmlHelper.ValidationMessageFor(expression, validationMessage, htmlAttributes, tag);

            var nameReg = new Regex("\\sdata-valmsg-for=\"[^\"]+\"");
            var name = TemplateForExtensions.GenerateName(htmlHelper, expression, customIndex);
            var stringResult = nameReg.Replace(result.ToString(), $" data-valmsg-for=\"{name}\"");

            return MvcHtmlString.Create(stringResult);
        }
    }
}
