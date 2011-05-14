using System.Collections.Generic;
using System.Linq;

namespace AskMe.Model.Specs
{
    public static class Ask
    {
        public static Question Question(string text)
        {
            return new Question(text, text, new List<Answer>());
        }

        public static Question WithAnswer(this Question question, string code, string text)
        {
            var newAnswers = question.Answers.Values.ToList();
            newAnswers.Add(new Answer(code, text, 0));
            return new Question(question.Code, question.Text, newAnswers);
        }
    }
}