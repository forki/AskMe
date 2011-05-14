namespace AskMe.Model
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
            get { return 5; }
        }

        public Item Item { get; private set; }

        public Answer SelectedAnswer { get; private set; }
    }
}