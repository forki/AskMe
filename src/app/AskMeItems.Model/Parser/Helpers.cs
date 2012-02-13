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
                         GetTextIfIndentedOrEmpty(lines, ref lineNo)
                       : defaultValue;
        }

        public static string GetTextIfIndentedOrEmpty(IList<string> lines, ref int lineNo)
        {
            if (lines.Count > lineNo && (lines[lineNo].StartsWith("  ") || lines[lineNo].RemoveLineBreaks() == String.Empty))
                return "\r\n" + lines[lineNo++].TrimStart(' ').RemoveLineBreaks() +
                       GetTextIfIndentedOrEmpty(lines, ref lineNo);
            return String.Empty;
        }

        public static bool TextIsEmptyOrWhitespace(string line)
        {
            return String.IsNullOrEmpty(line) || line.Replace("\t", "").Replace(" ", "") == String.Empty;
        }
    }
}