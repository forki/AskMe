using System;
using System.Collections.Generic;
using System.Linq;

namespace AskMeItems.Model.Specs
{
    public static class Ask
    {
        public static Questionnaire Item(string code, string text)
        {
            return
                new Questionnaire(new List<Item>())
                    .Item(code, text);
        }

        public static Questionnaire Item(string text)
        {
            return Item(text, text);
        }

        public static Questionnaire Item(this Questionnaire questionnaire, string code, string text)
        {
            var questions = questionnaire.Items.ToList();
            questions.Add(new Item(code, text, new List<Answer>()));
            return new Questionnaire(questions);
        }

        public static Questionnaire Item(this Questionnaire questionnaire, string text)
        {
            return questionnaire.Item(text, text);
        }

        public static QuestionnairePresenter ToPresenter(this Questionnaire questionnaire)
        {
            return new QuestionnairePresenter(questionnaire);
        }

        public static Questionnaire WithAnswer(this Questionnaire questionnaire, string code, string text)
        {
            var questions = questionnaire.Items.ToList();
            questions[questions.Count - 1] =
                questions[questions.Count - 1]
                    .WithAnswer(code, text);

            return new Questionnaire(questions);
        }

        public static Item WithAnswer(this Item item, string code, string text)
        {
            var newAnswers = item.Answers.Values.ToList();
            newAnswers.Add(new Answer(code, text, 0));

            return new Item(item.Code, item.Text, newAnswers);
        }
    }
}