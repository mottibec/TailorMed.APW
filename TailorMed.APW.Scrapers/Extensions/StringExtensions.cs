using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TailorMed.APW.Scrapers.Extensions
{
    public static class StringExtensions
    {
        private static char[] _trimAll = new char[]
        {
            '\n',
            '\t',
            ' '
        };

        public static string CleanHtmlContent(this string html)
            => html.TrimStart(_trimAll)
            .TrimEnd(_trimAll);
    }
}
