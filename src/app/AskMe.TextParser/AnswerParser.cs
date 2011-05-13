using System.Collections.Generic;
using System.Linq;
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

        private static Answer BuildAnswer(string text)
        {
            string[] answerParts = text.Split(')');
            return new Answer(answerParts[0].TrimStart(' '), answerParts[1].TrimStart(' '));
        }
    }
}