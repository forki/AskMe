namespace AskMeItems.Model
{
    public class Result
    {
        public Result(Item item, Answer answer)
        {
            Item = item;
            SelectedAnswer = answer;
        }

        public Item Item { get; private set; }

        public Answer SelectedAnswer { get; private set; }
    }
}