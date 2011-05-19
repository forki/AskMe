namespace AskMeItems.Model
{
    public class Result
    {
        public Result(Item item, Answer answer)
        {
            Item = item;
            SelectedAnswer = answer;
        }

        public int Points
        {
            get { return SelectedAnswer.Points; }
        }

        public Item Item { get; private set; }

        public Answer SelectedAnswer { get; private set; }
    }
}