using System;
using System.Collections.Generic;
using System.Linq;

using AskMeItems.Model;

namespace AskMeItems.TextParser
{
    public class QuestionnaireParser
    {
        public Questionnaire Parse(string text)
        {
            var lines = text.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            var lineNo = 0;
            var questions = new List<Item>();
            var questionCount = 0;
            while (ItemParser.HasNextQuestion(lines, lineNo))
                questions.Add(ItemParser.Parse(lines, questionCount++, ref lineNo));
            return new Questionnaire(questions);
        }
    }
}