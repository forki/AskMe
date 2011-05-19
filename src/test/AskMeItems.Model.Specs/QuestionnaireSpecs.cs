using System;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_adding_two_items_with_same_code
    {
        static Questionnaire Questionnaire;
        static Exception Exception;

        Establish context =
            () => Questionnaire = Ask.Item("HADS_1", "How do you feel?");

        Because of =
            () =>
            Exception =
            Catch.Exception(() => Questionnaire.Item("HADS_1", "How do you really feel?"));

        It should_give_a_nice_error = () => Exception.ShouldBeOfType<DuplicateItemException>();
        It should_give_a_nice_error_message = () => Exception.Message.ShouldEqual("The item HADS_1 was used twice.");
    }

    public class when_using_a_questionaire_with_two_items
    {
        protected static Questionnaire Questionnaire;

        Establish context =
            () => Questionnaire =
                  Ask.Item("HADS_1", "How do you feel?")
                      .WithAnswer("A", "good",1)
                      .WithAnswer("B", "bad",2)
                      .Item("HADS_2", "How do you really feel?")
                      .WithAnswer("A", "very good",3)
                      .WithAnswer("B", "very bad",4);
    }
}