namespace AskMe.Model
{
    public class Answer
    {
        public int Points { get; private set; }

        public Answer(string code, string text, int points)
        {
            Code = code;
            Text = text;
            Points = points;
        }

        public string Text { get; private set; }

        public string Code { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}) {1}", Code, Text);
        }
    }
}