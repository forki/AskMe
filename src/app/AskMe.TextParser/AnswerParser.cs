using System;
using System.Collections.Generic;
using AskMe.Model;

namespace AskMe.TextParser
{
    public class AnswerParser
    {
        public static List<Answer> ParseAnswers(List<string> answerLines, ref int lineNo)
        {
            var answers = new List<Answer>();
            while (lineNo < answerLines.Count && answerLines[lineNo].StartsWith("  "))
                answers.Add(BuildAnswer(answerLines[lineNo++]));

            return answers;
        }

        private static Answer BuildAnswer(string line)
        {
            string[] answerParts = line.Split(')');
            string code = answerParts[0].Trim(' ');
            string[] textParts = answerParts[1].Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries);
            string text = textParts[0].Trim(' ');
            int points = 0;
            if (textParts.Length > 1)
                points = int.Parse(textParts[1].Trim(' '));

            return new Answer(code, text, points);
        }
    }
}