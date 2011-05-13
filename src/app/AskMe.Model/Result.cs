namespace AskMe.Model
{
    public class Result
    {
        public Result(Question question, Answer answer)
        {
            Question = question;
            SelectedAnswer = answer;
        }

        public int Points
        {
            get { return 5; }
        }

        public Question Question { get; private set; }

        public Answer SelectedAnswer { get; private set; }
    }
}