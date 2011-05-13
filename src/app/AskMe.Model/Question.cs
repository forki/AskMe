using System;
using System.Collections.Generic;

namespace AskMe.Model
{
    public class Question
    {
        public Question(string text, List<Answer> answers)
        {
            Text = text;
            Answers = answers;
        }

        public string Text { get; private set; }

        public List<Answer> Answers { get; private set; }
    }
}