using Gorilla.Utilities;
using Gorilla.Utilities.Web.Mvc;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Create a input for Angular ngRepeat
    /// </summary>
    internal class TemplateForExtensions
    {
        /// <summary>
        /// Create a input for Angular ngRepeat
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Lambda for model</param>
        /// <param name="htmlAttributes">Additional html attrs</param>
        /// <param name="inputType">Type of input</param>
        /// <param name="numberReplace">number of replacements</param>
        /// <param name="value">Current value for input</param>
        /// <returns></returns>
        public static MvcHtmlString TemplateFor<TModel, TValue>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, string inputType, int? numberReplace = null, string value = "")
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var fullName = GenerateName(htmlHelper, expression, numberReplace);
            var tagBuilder = inputType == "textarea" ? TemplateBaseForTextArea(value) : TemplateBaseForInput(inputType, value);

            tagBuilder.MergeAttribute("id", GenerateId(fullName));
            tagBuilder.MergeAttribute("name", fullName, true);

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(htmlFieldName, metadata));

            if (htmlAttributes == null)
            {
                return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
            }

            foreach (var attrs in htmlAttributes)
            {
                tagBuilder.MergeAttribute(attrs.Key.Replace("_", "-"), attrs.Value.ToString(), true);
            }

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        private static TagBuilder TemplateBaseForInput(string inputType, string value)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", inputType);
            tagBuilder.MergeAttribute("value", value);

            return tagBuilder;
        }


        private static TagBuilder TemplateBaseForTextArea(string value)
        {
            var tagBuilder = new TagBuilder("textarea")
            {
                InnerHtml = value
            };

            return tagBuilder;
        }


        /// <summary>
        /// Generate a name for input
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="numberReplace"></param>
        /// <returns></returns>
        public static string GenerateName(HtmlHelper htmlHelper, LambdaExpression expression, int? numberReplace)
        {
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            return GenerateName(htmlHelper, htmlFieldName, numberReplace);
        }

        /// <summary>
        /// Generate a name for input
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlFieldName"></param>
        /// <param name="numberReplace"></param>
        /// <returns></returns>
        private static string GenerateName(HtmlHelper htmlHelper, string htmlFieldName, int? numberReplace)
        {

            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);

            var find = new Regex(@"\[\d+\]");
            var replace = TemplateSettings.Index;

            var z = 0;
            while (find.Matches(fullName).Count > 0)
            {
                fullName = fullName.ReplaceRegLastOccurrence(@"\[\d+\]", string.Format($"[{TemplateSettings.Pattern}]", replace));
                replace = string.Format(TemplateSettings.ParentIndex, replace);

                z++;
                if (z == 100 || (numberReplace != null && z == numberReplace))
                    break;
            }

            return fullName;
        }

        /// <summary>
        /// Generate the id based on name
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        private static string GenerateId(string fullName)
        {
            return fullName.Replace("[", "_").Replace("]", "_").Replace(".", "_").Replace("_$", ".$");
        }

        internal static string ReplaceNameAndId(string stringResult, HtmlHelper htmlHelper, LambdaExpression expression, int? numberReplace)
        {
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            return ReplaceNameAndId(stringResult, htmlHelper, htmlFieldName, numberReplace);
        }

        internal static string ReplaceNameAndId(string stringResult, HtmlHelper htmlHelper, string htmlFieldName, int? numberReplace)
        {
            var nameReg = new Regex("\\sname=\"[^ \"]+\"");
            var name = GenerateName(htmlHelper, htmlFieldName, numberReplace);
            stringResult = nameReg.Replace(stringResult, $" name=\"{name}\"");

            var idReg = new Regex("\\sid=\"[^ \"]+\"");
            var id = GenerateId(name);
            stringResult = idReg.Replace(stringResult, $"id=\"{id}\"");

            return stringResult;
        }
    }
}