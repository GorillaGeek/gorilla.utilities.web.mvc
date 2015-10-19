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
    public static class TemplateForExtensions
    {
        public static MvcHtmlString DropDownListTemplateFor<TModel, TEnum>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression,
            IEnumerable<SelectListItem> selectList,
            object htmlAttributes = null)
        {
            return DropDownListTemplateFor(htmlHelper, expression, null, htmlAttributes);
        }

        public static MvcHtmlString DropDownListTemplateFor<TModel, TEnum>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TEnum>> expression,
           IEnumerable<SelectListItem> selectList,
           string optionLabel = null,
           object htmlAttributes = null)
        {
            var result = htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
            var stringResult = TemplateForExtensions.ReplaceNameAndId(result.ToString(), htmlHelper, expression, null);

            return MvcHtmlString.Create(stringResult);
        }

        public static MvcHtmlString EnumDropDownListTemplateFor<TModel, TEnum>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression,
            object htmlAttributes = null)
        {
            return EnumDropDownListTemplateFor(htmlHelper, expression, null, htmlAttributes);
        }

        public static MvcHtmlString EnumDropDownListTemplateFor<TModel, TEnum>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TEnum>> expression,
           string optionLabel = null,
           object htmlAttributes = null)
        {
            var result = htmlHelper.EnumDropDownListFor(expression, optionLabel, htmlAttributes);
            var stringResult = TemplateForExtensions.ReplaceNameAndId(result.ToString(), htmlHelper, expression, null);

            return MvcHtmlString.Create(stringResult);
        }

        public static MvcHtmlString HiddenTemplateFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null,
            int? numberReplace = null)
        {
            var result = htmlHelper.HiddenFor(expression, htmlAttributes);
            var stringResult = ReplaceNameAndId(result.ToString(), htmlHelper, expression, numberReplace);

            return MvcHtmlString.Create(stringResult);
        }

        public static MvcHtmlString CheckBoxTemplateFor<TModel, TValue>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, bool>> expression,
           object htmlAttributes = null,
           int? numberReplace = null)
        {
            var result = htmlHelper.CheckBoxFor(expression, htmlAttributes);
            var stringResult = ReplaceNameAndId(result.ToString(), htmlHelper, expression, numberReplace);

            return MvcHtmlString.Create(stringResult);
        }

        public static MvcHtmlString RadioButtonTemplateFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object value,
            object htmlAttributes = null,
            int? numberReplace = null)
        {
            var result = htmlHelper.RadioButtonFor(expression, value, htmlAttributes);
            var stringResult = ReplaceNameAndId(result.ToString(), htmlHelper, expression, numberReplace);

            return MvcHtmlString.Create(stringResult);
        }

        public static MvcHtmlString TextAreaTemplateFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null,
            int? numberReplace = null)
        {
            var result = htmlHelper.TextAreaFor(expression, htmlAttributes);
            var stringResult = ReplaceNameAndId(result.ToString(), htmlHelper, expression, numberReplace);

            return MvcHtmlString.Create(stringResult);
        }


        public static MvcHtmlString TextBoxTemplateFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null,
            int? numberReplace = null)
        {
            var result = htmlHelper.TextBoxFor(expression, htmlAttributes);
            var stringResult = ReplaceNameAndId(result.ToString(), htmlHelper, expression, numberReplace);

            return MvcHtmlString.Create(stringResult);
        }

        public static MvcHtmlString ValidationMessageTemplateFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string validationMessage = null,
            object htmlAttributes = null,
            string tag = "span",
            int? numberReplace = null)
        {
            var result = htmlHelper.ValidationMessageFor(expression, validationMessage, htmlAttributes, tag);

            var nameReg = new Regex("\\sdata-valmsg-for=\"[^\"]+\"");
            var name = GenerateName(htmlHelper, expression, numberReplace);
            var stringResult = nameReg.Replace(result.ToString(), $" data-valmsg-for=\"{name}\"");

            return MvcHtmlString.Create(stringResult);
        }

        private static string ReplaceNameAndId(string stringResult, HtmlHelper htmlHelper, LambdaExpression expression, int? numberReplace)
        {
            var nameReg = new Regex("\\sname=\"[^\"]+\"");
            var name = GenerateName(htmlHelper, expression, numberReplace);
            stringResult = nameReg.Replace(stringResult, $" name=\"{name}\"");

            var idReg = new Regex("\\sid=\"[^\"]+\"");
            var id = GenerateId(name);
            stringResult = idReg.Replace(stringResult, $"id=\"{id}\"");

            return stringResult;
        }

        private static string GenerateName(HtmlHelper htmlHelper, LambdaExpression expression, int? numberReplace)
        {
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);

            var find = new Regex(@"\[\d+\]");
            var replace = TemplateSettings.Index;

            var z = 0;
            while (find.Matches(fullName).Count > 0)
            {
                fullName = fullName.ReplaceRegLastOccurrence(@"\[\d+\]", String.Format($"[{TemplateSettings.Pattern}]", replace));
                replace = String.Format(TemplateSettings.ParentIndex, replace);

                z++;
                if (z == 100 || (numberReplace != null && z == numberReplace))
                    break;
            }

            return fullName;
        }

        private static string GenerateId(string fullName)
        {
            return fullName.Replace("[", "_").Replace("]", "_").Replace(".", "_").Replace("_$", ".$");
        }


    }
}