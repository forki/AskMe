using System;
using System.Linq;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_answering_a_question
    {
        static Item Item;
        protected static Result Result;

        Establish context =
            () => Item =
                  Ask.Item("How do you feel?")
                      .WithAnswer("A", "good")
                      .WithAnswer("B", "bad")
                      .Items.Last();

        Because of = () => Result = Item.AnswerWith("A");

        It should_give_5_points = () => Result.Points.ShouldEqual(5);
        It should_be_an_answer_to_the_question = () => Result.Item.ShouldEqual(Item);
        It should_be_the_given_answer = () => Result.SelectedAnswer.Code.ShouldEqual("A");
    }

    public class when_answering_a_question_with_stupid_input
    {
        static Item Item;
        protected static Result Result;
        static Exception Exception;

        Establish context =
            () => Item =
                  Ask.Item("How do you feel?")
                      .WithAnswer("A", "good")
                      .WithAnswer("B", "bad")
                      .Items.Last();

        Because of = () => Exception = Catch.Exception(() => Result = Item.AnswerWith("C"));

        It should_give_a_nice_error = () => Exception.ShouldBeOfType<AnswerNotAllowedException>();
        It should_give_a_nice_error_message = () => Exception.Message.ShouldEqual("The answer C is not allowed.");
    }
}