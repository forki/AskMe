using System;
using System.Linq;

using AskMeItems.Model.Specs.Data;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_answering_a_question
    {
        static Item Item;
        protected static Result Result;

        Establish context =
            () => Item =
                  Ask.Item("Test1", "How do you feel?")
                      .WithAnswer("A", "good", 5)
                      .WithAnswer("B", "bad", 2)
                      .Items.Last();

        public class when_answering_with_A
        {
            Because of = () => Result = Item.AnswerWith("A");

            It should_give_5_points = () => Result.SelectedAnswer.Points.ShouldEqual(5);
            It should_be_an_answer_to_the_question = () => Result.Item.ShouldEqual(Item);
            It should_be_the_given_answer = () => Result.SelectedAnswer.Code.ShouldEqual("A");
        }

        public class when_answering_with_B
        {
            Because of = () => Result = Item.AnswerWith("B");

            It should_give_5_points = () => Result.SelectedAnswer.Points.ShouldEqual(2);
            It should_be_an_answer_to_the_question = () => Result.Item.ShouldEqual(Item);
            It should_be_the_given_answer = () => Result.SelectedAnswer.Code.ShouldEqual("B");
        }

        public class when_answering_a_question_with_stupid_input
        {
            static Exception Exception;

            Because of = () => Exception = Catch.Exception(() => Item.AnswerWith("C"));

            It should_give_a_nice_error =
                () => Exception.ShouldBeOfType<AnswerNotAllowedException>();

            It should_give_a_nice_error_message =
                () => Exception.Message.ShouldEqual("The answer C is not allowed for item Test1.");
        }
    }
}