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
            var questionaire = new Questionaire();
            while (QuestionParser.HasNextQuestion(lines, lineNo))
                questionaire.AddQuestion(QuestionParser.Parse(lines, ref lineNo));
            return questionaire;
        }
    }
}