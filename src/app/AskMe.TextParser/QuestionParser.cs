using System.Collections.Generic;
using AskMe.Model;

namespace AskMe.TextParser
{
    public class QuestionParser
    {
        public static bool HasNextQuestion(List<string> lines, int lineNo)
        {
            return lineNo < lines.Count;
        }

        public static Question Parse(List<string> lines, ref int lineNo)
        {
            string text = lines[lineNo++];
            return new Question(text, AnswerParser.ParseAnswers(lines, ref lineNo));
        }
    }
}