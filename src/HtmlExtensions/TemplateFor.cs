using Gorilla.Utilities;
using Gorilla.Utilities.Web.Mvc;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Create a input for Angular ngRepeat
    /// </summary>
    internal static class TemplateForExtensions
    {
        public static string ReplaceNameAndId(string stringResult, HtmlHelper htmlHelper, LambdaExpression expression, string customIndex)
        {
            var nameReg = new Regex("\\sname=\"[^\"]+\"");
            var name = GenerateName(htmlHelper, expression, customIndex);
            stringResult = nameReg.Replace(stringResult, $" name=\"{name}\"");

            var idReg = new Regex("\\sid=\"[^\"]+\"");
            var id = GenerateId(name);
            stringResult = idReg.Replace(stringResult, $"id=\"{id}\"");

            return stringResult;
        }

        public static string GenerateName(HtmlHelper htmlHelper, LambdaExpression expression, string customIndex)
        {
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);

            var find = new Regex(@"\[\d+\]");
            var replace = customIndex ?? TemplateSettings.Index;

            while (find.Matches(fullName).Count > 0)
            {
                fullName = fullName.ReplaceRegLastOccurrence(@"\[\d+\]", string.Format($"[{TemplateSettings.Pattern}]", replace));
                replace = string.Format(TemplateSettings.ParentIndex, replace);
            }

            return fullName;
        }

        public static string GenerateId(string fullName)
        {
            return fullName.Replace("[", "_").Replace("]", "_").Replace(".", "_").Replace("_$", ".$");
        }


    }
}