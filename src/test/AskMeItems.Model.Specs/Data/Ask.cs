using System;
using System.Collections.Generic;
using System.Linq;

using AskMeItems.Model.Export;

namespace AskMeItems.Model.Specs.Data
{
    public static class Ask
    {
        public static Questionnaire NewQuestionnaire(string code)
        {
            return new Questionnaire(code, QuestionnaireType.ListedAnswers, "", new List<Item>());
        }

        public static Questionnaire Item(this Questionnaire questionnaire, string code, string text)
        {
            var questions = questionnaire.Items.ToList();
            questions.Add(new Item(code, text, new List<Answer>()));
            return new Questionnaire(questionnaire.Code, questionnaire.Type, questionnaire.Instruction, questions);
        }

        public static QuestionnairePresenter AnswerWith(this QuestionnairePresenter presenter, string answerCode)
        {
            presenter.AnswerCurrentItem(presenter.CurrentItem.Answers[answerCode]);
            return presenter;
        }

        public static Questionnaire WithAnswer(this Questionnaire questionnaire, string code, string text, int points)
        {
            var questions = questionnaire.Items.ToList();
            questions[questions.Count - 1] =
                questions[questions.Count - 1]
                    .WithAnswer(code, text, points);

            return new Questionnaire(questionnaire.Code, questionnaire.Type, questionnaire.Instruction, questions);
        }

        public static Item WithAnswer(this Item item, string code, string text, int points)
        {
            var newAnswers = item.Answers.Values.ToList();
            newAnswers.Add(new Answer(code, text, points));

            return new Item(item.Code, item.Text, newAnswers);
        }

        public static string[] SplitOnLineBreaks(this string text)
        {
            return text.Split(new[] {"\r\n"}, StringSplitOptions.None);
        }

        public static Subscale ByName(this IEnumerable<Subscale> subscales, string name)
        {
            return subscales
                .Where(s => s.Name == name)
                .First();
        }

        public static QuestionnairePresenter ToPresenter(this Questionnaire questionnaire, string subjectCode)
        {
            return new QuestionnairePresenter(new CSVExporter(), subjectCode, questionnaire);
        }
    }
}