using System.Collections.Generic;

namespace AskMe.Model
{
    public class Question
    {
        public Question(string text, IEnumerable<Answer> answers)
        {
            Text = text;

            Answers = new Dictionary<string, Answer>();
            foreach (Answer answer in answers)
                Answers.Add(answer.Code, answer);
        }

        public string Text { get; private set; }

        public Dictionary<string, Answer> Answers { get; private set; }

        public Result Answer(string code)
        {
            return new Result(this, Answers[code]);
        }
    }
}