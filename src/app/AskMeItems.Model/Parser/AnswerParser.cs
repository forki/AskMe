using System;
using System.Collections.Generic;

namespace AskMeItems.Model.Parser
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

        static Answer BuildAnswer(string line)
        {
            var answerParts = line.Split(')');
            var code = answerParts[0].Trim(' ');
            var textParts = answerParts[1].Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries);
            var text = textParts[0].Trim(' ');
            var points = 0;
            if (textParts.Length > 1)
                points = int.Parse(textParts[1].Trim(' '));

            return new Answer(code, text, points);
        }
    }
}