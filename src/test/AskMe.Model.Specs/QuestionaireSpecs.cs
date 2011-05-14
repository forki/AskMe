using System;

using Machine.Specifications;

namespace AskMe.Model.Specs
{
    public class when_adding_two_questions_with_same_code
    {
        static Questionaire Questionaire;
        static Exception Exception;

        Establish context =
            () => Questionaire = Ask.Question("HADS_1", "How do you feel?");

        Because of =
            () =>
            Exception =
            Catch.Exception(() => Questionaire.Question("HADS_1", "How do you really feel?"));

        It should_give_a_nice_error = () => Exception.ShouldBeOfType<DuplicateQuestionException>();
        It should_give_a_nice_error_message = () => Exception.Message.ShouldEqual("The question HADS_1 was used twice.");
    }
}