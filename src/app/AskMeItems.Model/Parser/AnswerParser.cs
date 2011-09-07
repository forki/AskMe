using System;
using System.Collections.Generic;

namespace AskMeItems.Model.Parser
{
    public class AnswerParser
    {
        public static List<Answer> ParseAnswers(List<string> answerLines, ref int lineNo)
        {
            var answers = new List<Answer>();
            while (lineNo < answerLines.Count && StartsWithIndent(answerLines[lineNo]))
                answers.Add(BuildAnswer(answerLines[lineNo++]));

            return answers;
        }

        static bool StartsWithIndent(string text)
        {
            return text.StartsWith("  ") || text.StartsWith("\t");
        }

        static Answer BuildAnswer(string line)
        {
            var answerParts = line.Split(')');
            var code = answerParts[0].Trim(' ').Trim('\t');
            var textParts = answerParts[1].Split(new[] {" - "}, StringSplitOptions.None);
            var text = textParts[0].Trim(' ');
            var points = ParsePoints(textParts, code);

            return new Answer(code, text, points);
        }

        static int ParsePoints(IList<string> textParts, string code)
        {
            int points;
            int.TryParse(code, out points);
            if (textParts.Count > 1)
                points = int.Parse(textParts[1].Trim(' '));
            return points;
        }
    }
}