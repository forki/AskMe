using System;

namespace AskMe.Model
{
    public class Answer
    {
        public Answer(string code, string text)
        {
            Code = code;
            Text = text;
        }

        public string Text { get; private set; }

        public string Code { get; private set; }
        public override string ToString()
        {
            return string.Format("{0}) {1}", Code, Text);
        }
    }
}