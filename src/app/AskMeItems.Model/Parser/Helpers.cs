using System;
using System.Collections.Generic;

namespace AskMeItems.Model.Parser
{
    static class Helpers
    {
        public static string RemoveLineBreaks(this string text)
        {
            return text.Replace("\n", "").Replace("\r", "");
        }

        public static string ParseProperty(string propertyName, IList<string> lines, ref int lineNo, string defaultValue)
        {
            var pattern = propertyName + ": ";
            return lines[lineNo].StartsWith(pattern)
                       ? lines[lineNo++].Replace(pattern, "").RemoveLineBreaks() +
                         GetTextIfIndented(lines, ref lineNo)
                       : defaultValue;
        }

        public static string GetTextIfIndented(IList<string> lines, ref int lineNo)
        {
            if (lines.Count > lineNo && lines[lineNo].StartsWith("  "))
                return "\r\n" + lines[lineNo++].TrimStart(' ').RemoveLineBreaks() +
                       GetTextIfIndented(lines, ref lineNo);
            return String.Empty;
        }
    }
}