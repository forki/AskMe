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
            var type = ParseProperty("Questionnaire-Type", ref lineNo, lines);

            while (ItemParser.HasNextQuestion(lines, lineNo))
                questions.Add(ItemParser.Parse(lines, questionCount++, ref lineNo));
            return new Questionnaire(type, questions);
        }

        static QuestionnaireType ParseProperty(string propertyName, ref int lineNo, List<string> lines)
        {
            var pattern = propertyName + ": ";
            var type = QuestionnaireType.ListedAnswers;
            if (lines[lineNo].StartsWith(pattern))
                Enum.TryParse(lines[lineNo++].Replace(pattern, ""), out type);
            return type;
        }
    }
}