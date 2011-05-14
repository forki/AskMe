using System.Collections.Generic;
using System.Linq;

namespace AskMe.Model.Specs
{
    public static class Ask
    {
        public static Questionaire Question(string code, string text)
        {
            return
                new Questionaire(new List<Question>())
                    .Question(code, text);
        }

        public static Questionaire Question(string text)
        {
            return Question(text, text);
        }

        public static Questionaire Question(this Questionaire questionaire, string code, string text)
        {
            var questions = questionaire.Questions.ToList();
            questions.Add(new Question(code, text, new List<Answer>()));
            return new Questionaire(questions);
        }

        public static Questionaire Question(this Questionaire questionaire, string text)
        {
            return questionaire.Question(text, text);
        }

        public static Questionaire WithAnswer(this Questionaire questionaire, string code, string text)
        {
            var questions = questionaire.Questions.ToList();
            questions[questions.Count - 1] =
                questions[questions.Count - 1]
                    .WithAnswer(code, text);

            return new Questionaire(questions);
        }

        public static Question WithAnswer(this Question question, string code, string text)
        {
            var newAnswers = question.Answers.Values.ToList();
            newAnswers.Add(new Answer(code, text, 0));

            return new Question(question.Code, question.Text, newAnswers);
        }
    }
}