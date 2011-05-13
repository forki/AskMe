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
    }
}