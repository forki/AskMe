using System;
using System.Collections.Generic;
using System.Linq;
using AskMe.Model;

namespace AskMe.TextParser
{
    public class QuestionaireParser
    {
        public Questionaire Parse(string text)
        {
            List<string> lines = text.Split(new[] {"\r","\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            int lineNo = 0;
            var questions = new List<Item>();
            int questionCount = 0;
            while (ItemParser.HasNextQuestion(lines, lineNo))
                questions.Add(ItemParser.Parse(lines, questionCount++, ref lineNo));
            return new Questionaire(questions);
        }
    }
}