using System.Collections.Generic;
using System.Text.RegularExpressions;

using AskMe.Model;

namespace AskMe.TextParser
{
    public class QuestionParser
    {
        static readonly Regex QuestionRegex = new Regex(@"(([^\s]+):\s)?(.*)");

        public static bool HasNextQuestion(List<string> lines, int lineNo)
        {
            return lineNo < lines.Count;
        }

        public static Question Parse(List<string> lines, int questionCount, ref int lineNo)
        {
            string text = lines[lineNo++];
            var m = QuestionRegex.Match(text);

            string code = m.Groups[2].Value;
            if (string.IsNullOrEmpty(code))
                code = string.Format("Q_{0}", questionCount);
            return new Question(code, m.Groups[3].Value, AnswerParser.ParseAnswers(lines, ref lineNo));
        }
    }
}