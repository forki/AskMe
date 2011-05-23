using System.Collections.Generic;

using Machine.Specifications;

namespace AskMeItems.Model.Specs.Data
{
    public class when_using_a_questionaire_with_two_items
    {
        protected static Questionnaire Questionnaire;

        Establish context =
            () => Questionnaire =
                  Ask.Item("HADS_1", "How do you feel?")
                      .WithAnswer("A", "good", 1)
                      .WithAnswer("B", "bad", 2)
                      .Item("HADS_2", "How do you really feel?")
                      .WithAnswer("A", "very good", 3)
                      .WithAnswer("B", "very bad", 4);
    }

    public class when_using_an_answered_questionaire_with_two_items : when_using_a_questionaire_with_two_items
    {
        protected static QuestionnairePresenter Presenter;

        Establish context =
            () =>
            Presenter =
            Questionnaire
                .ToPresenter()
                .AnswerWith("A")
                .AnswerWith("B");
    }

    public class when_using_a_questionaire_with_three_subscales
    {
        protected static Questionnaire Questionnaire;

        protected static List<Subscale> Subscales;

        Establish context =
            () => Questionnaire =
                  Ask.Item("A_1", "How do you feel?")
                      .WithAnswer("A", "good", 1)
                      .WithAnswer("B", "bad", 2)
                      .Item("A_2", "How do you really feel?")
                      .WithAnswer("A", "very good", 1)
                      .WithAnswer("B", "very bad", 2)
                      .Item("B_1", "How do you feel?")
                      .WithAnswer("A", "good", 3)
                      .WithAnswer("B", "bad", 4)
                      .Item("B_2", "How do you really feel?")
                      .WithAnswer("A", "very good", 2)
                      .WithAnswer("B", "very bad", 4)
                      .Item("B_3", "How do you really feel?")
                      .WithAnswer("A", "very good", 5)
                      .WithAnswer("B", "very bad", 0)
                      .Item("C_1", "How do you feel?")
                      .WithAnswer("A", "good", 7)
                      .WithAnswer("B", "bad", 8);
    }
}