using System.Collections.Generic;
using System.Linq;

namespace AskMe.Model.Specs
{
    public static class Ask
    {
        public static Question Question(string text)
        {
            return new Question(text,new List<Answer>());
        }

        public static Question WithAnswer(this Question question, string code, string text)
        {
            var newAnswers = question.Answers.ToList();
            newAnswers.Add(new Answer(code,text));
            return new Question(question.Text,newAnswers);
        }
    }
}