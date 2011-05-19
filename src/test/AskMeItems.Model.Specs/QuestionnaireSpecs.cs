using System;

using AskMeItems.Model.Specs.Data;

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
}