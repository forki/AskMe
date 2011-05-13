namespace AskMe.Model
{
    public class Result
    {
        public Result(int points)
        {
            Points = points;
        }

        public int Points { get; private set; }
    }
}