using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Nop.Core.Html.CodeFormatter
{
    /// <summary>
    /// Represents a code format helper
    /// </summary>
    public partial class CodeFormatHelper
    {
        #region Fields
        //private static Regex regexCode1 = new Regex(@"(?<begin>\[code:(?<lang>.*?)(?:;ln=(?<linenumbers>(?:on|off)))?(?:;alt=(?<altlinenumbers>(?:on|off)))?(?:;(?<title>.*?))?\])(?<code>.*?)(?<end>\[/code\])", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private readonly static Regex regexHtml = new Regex("<[^>]*>", RegexOptions.Compiled);
        private readonly static Regex regexCode2 = new Regex(@"\[code\](?<inner>(.*?))\[/code\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        #endregion
    }
}

