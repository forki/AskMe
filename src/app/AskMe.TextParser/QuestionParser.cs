using System.Collections.Generic;
using System.Text.RegularExpressions;

using AskMe.Model;

namespace AskMe.TextParser
{
    public class QuestionParser
    {
        static readonly Regex QuestionRegex = new Regex(@"(([^\s]+):\s)?(.*)", RegexOptions.Compiled);

        public static bool HasNextQuestion(List<string> lines, int lineNo)
        {
            return lineNo < lines.Count;
        }

        public static Question Parse(List<string> lines, int questionCount, ref int lineNo)
        {
            var text = lines[lineNo++];
            var match = QuestionRegex.Match(text);

            var code = BuildQuestionCode(questionCount, match);
            return new Question(code, match.Groups[3].Value, AnswerParser.ParseAnswers(lines, ref lineNo));
        }

        static string BuildQuestionCode(int questionCount, Match match)
        {
            var code = match.Groups[2].Value;
            if (string.IsNullOrEmpty(code))
                code = string.Format("Q_{0}", questionCount);
            return code;
        }
    }
}