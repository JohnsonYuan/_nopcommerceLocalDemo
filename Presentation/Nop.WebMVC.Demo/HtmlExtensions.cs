using System;
using System.Web.Mvc;

namespace Nop.WebMVC.Demo
{
    public static class HtmlExtensions
    {
        public static string FieldIdFor<T, TResult>(this HtmlHelper<T> html, System.Linq.Expressions.Expression<Func<T, TResult>> expression)
        {
            var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            // because "[" and "]" aren't replaced with "_" in GetFullHtmlFieldId
            return id.Replace('[', '_').Replace(']', '_');
        }
    }
}