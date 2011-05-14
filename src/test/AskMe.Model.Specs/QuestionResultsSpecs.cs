using System;

using Machine.Specifications;

namespace AskMe.Model.Specs
{
    public class when_answering_a_question
    {
        static Question Question;
        protected static Result Result;

        Establish context =
            () => Question =
                  Ask.Question("How do you feel?")
                      .WithAnswer("A", "good")
                      .WithAnswer("B", "bad");

        Because of = () => Result = Question.AnswerWith("A");

        It should_give_5_points = () => Result.Points.ShouldEqual(5);
        It should_be_an_answer_to_the_question = () => Result.Question.ShouldEqual(Question);
        It should_be_the_given_answer = () => Result.SelectedAnswer.Code.ShouldEqual("A");
    }

    public class when_answering_a_question_with_stupid_input
    {
        static Question Question;
        protected static Result Result;

        Establish context =
            () => Question =
                  Ask.Question("How do you feel?")
                      .WithAnswer("A", "good")
                      .WithAnswer("B", "bad");

        Because of = () => Exception = Catch.Exception(() => Result = Question.AnswerWith("C"));

        static Exception Exception;

        It should_give_a_nice_error = () => Exception.ShouldBeOfType<AnswerNotAllowedException>();
        It should_give_a_nice_error_message = () => Exception.Message.ShouldEqual("The answer C is not allowed.");
    }
}