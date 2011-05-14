using System;
using System.Collections.Generic;
using System.Linq;

namespace AskMe.Model.Specs
{
    public static class Ask
    {
        public static Questionaire Item(string code, string text)
        {
            return
                new Questionaire(new List<Item>())
                    .Item(code, text);
        }

        public static Questionaire Item(string text)
        {
            return Item(text, text);
        }

        public static Questionaire Item(this Questionaire questionaire, string code, string text)
        {
            var questions = questionaire.Items.ToList();
            questions.Add(new Item(code, text, new List<Answer>()));
            return new Questionaire(questions);
        }

        public static Questionaire Item(this Questionaire questionaire, string text)
        {
            return questionaire.Item(text, text);
        }

        public static QuestionairePresenter ToPresenter(this Questionaire questionaire)
        {
            return new QuestionairePresenter(questionaire);
        }

        public static Questionaire WithAnswer(this Questionaire questionaire, string code, string text)
        {
            var questions = questionaire.Items.ToList();
            questions[questions.Count - 1] =
                questions[questions.Count - 1]
                    .WithAnswer(code, text);

            return new Questionaire(questions);
        }

        public static Item WithAnswer(this Item item, string code, string text)
        {
            var newAnswers = item.Answers.Values.ToList();
            newAnswers.Add(new Answer(code, text, 0));

            return new Item(item.Code, item.Text, newAnswers);
        }
    }
}