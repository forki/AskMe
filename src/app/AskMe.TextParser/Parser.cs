using System;
using System.Collections.Generic;
using AskMe.Model;

namespace AskMe.TextParser
{
    public class Parser
    {
        public List<Question> Parse(string text)
        {
            return new List<Question>{new Question(text)};
        }
    }
}