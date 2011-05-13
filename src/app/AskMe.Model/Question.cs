namespace AskMe.Model
{
    public class Question
    {
        public Question(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}