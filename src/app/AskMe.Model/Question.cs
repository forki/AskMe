using System.Collections.Generic;

namespace AskMe.Model
{
    public class Question
    {
        public Question(string code, string text, IEnumerable<Answer> answers)
        {
            Code = code;
            Text = text;

            Answers = new Dictionary<string, Answer>();
            foreach (var answer in answers)
                Answers.Add(answer.Code, answer);
        }

        public string Text { get; private set; }

        public Dictionary<string, Answer> Answers { get; private set; }

        public string Code { get; private set; }

        public Result AnswerWith(string answerCode)
        {
            Answer selectedAnswer;
            if (Answers.TryGetValue(answerCode, out selectedAnswer))
                return new Result(this, selectedAnswer);
            throw new AnswerNotAllowedException(answerCode);
        }
    }
}