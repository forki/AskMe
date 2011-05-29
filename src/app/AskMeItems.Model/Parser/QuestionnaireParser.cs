using System;
using System.Collections.Generic;
using System.Linq;

namespace AskMeItems.Model.Parser
{
    public class QuestionnaireParser
    {
        public Questionnaire Parse(string text)
        {
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            var lineNo = 0;
            var questions = new List<Item>();
            var questionCount = 0;
            QuestionnaireType type;
            Enum.TryParse(ParseProperty("Questionnaire-Type", ref lineNo, lines, "ListedAnswers"), out type);
            var instruction = ParseProperty("Instruction", ref lineNo, lines, null);

            while (ItemParser.HasNextQuestion(lines, lineNo))
                questions.Add(ItemParser.Parse(lines, questionCount++, ref lineNo));
            return new Questionnaire(type, instruction, questions);
        }

        static string ParseProperty(string propertyName, ref int lineNo, IList<string> lines, string defaultValue)
        {
            var pattern = propertyName + ": ";
            return lines[lineNo].StartsWith(pattern)
                       ? lines[lineNo++].Replace(pattern, "").RemoveLineBreaks() +
                         GetTextIfIndented(ref lineNo, lines)
                       : defaultValue;
        }

        static string GetTextIfIndented(ref int lineNo, IList<string> lines)
        {
            if (lines.Count > lineNo && lines[lineNo].StartsWith("  "))
                return "\r\n" + lines[lineNo++].TrimStart(' ').RemoveLineBreaks() +
                       GetTextIfIndented(ref lineNo, lines);
            return string.Empty;
        }
    }
}