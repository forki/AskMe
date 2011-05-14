using System;

using Machine.Specifications;

namespace AskMe.Model.Specs
{
    public class when_adding_two_answers_with_same_code
    {
        static Question Question;
        static Exception Exception;

        Establish context =
            () => Question = 
                Ask.Question("HADS_1", "How do you feel?")
                .WithAnswer("A", "Bad");

        Because of =
            () => Exception = Catch.Exception(() => Question.WithAnswer("A", "Bad again"));

        It should_give_a_nice_error = () => Exception.ShouldBeOfType<DuplicateAnswerException>();
        It should_give_a_nice_error_message = () => Exception.Message.ShouldEqual("The answer A was used twice in question HADS_1.");
    }
}