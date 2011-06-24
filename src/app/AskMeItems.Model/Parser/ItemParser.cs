using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AskMeItems.Model.Parser
{
    public class ItemParser
    {
        static readonly Regex ItemRegex = new Regex(@"([*]\s)?(([^\s]+):\s*)?(.*)", RegexOptions.Compiled);

        public static bool HasNextQuestion(List<string> lines, int lineNo)
        {
            return lineNo < lines.Count;
        }

        public static Item Parse(List<string> lines, int questionCount, ref int lineNo)
        {
            var text = lines[lineNo++];
            var match = ItemRegex.Match(text);

            var code = BuildQuestionCode(questionCount, match);
            var excludeFromSubscales = !string.IsNullOrEmpty(match.Groups[1].Value);
            var itemText = match.Groups[4].Value;
            return new Item(code, itemText, excludeFromSubscales, AnswerParser.ParseAnswers(lines, ref lineNo));
        }

        static string BuildQuestionCode(int questionCount, Match match)
        {
            var code = match.Groups[3].Value;
            if (string.IsNullOrEmpty(code))
                code = string.Format("Q_{0}", questionCount);
            return code;
        }
    }
}