using System;
using System.Collections.Generic;
using System.Linq;
using AskMe.Model;

namespace AskMe.TextParser
{
    public class QuestionaireParser
    {
        public List<Question> Parse(string text)
        {
            List<string> lines = text.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            int lineNo = 0;
            var questions = new List<Question>();
            while (QuestionParser.HasNextQuestion(lines, lineNo))
                questions.Add(QuestionParser.Parse(lines, ref lineNo));
            return questions;
        }
    }
}