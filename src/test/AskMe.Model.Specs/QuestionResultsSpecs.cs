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

        Because of = () => Result = Question.Answer("A");

        It should_give_5_points = () => Result.Points.ShouldEqual(5);
        It should_be_an_answer_to_the_question = () => Result.Question.ShouldEqual(Question);
        It should_be_the_given_answer = () => Result.SelectedAnswer.Code.ShouldEqual("A");
    }
}