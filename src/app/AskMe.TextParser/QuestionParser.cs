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

        public static Question Parse(List<string> lines, ref int lineNo)
        {
            string text = lines[lineNo++];
            var m = QuestionRegex.Match(text);

            return new Question(m.Groups[2].Value, m.Groups[3].Value, AnswerParser.ParseAnswers(lines, ref lineNo));
        }
    }
}