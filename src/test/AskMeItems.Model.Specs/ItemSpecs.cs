using System;
using System.Linq;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_adding_two_answers_with_same_code
    {
        static Item Item;
        static Exception Exception;

        Establish context =
            () => Item =
                  Ask.Item("HADS_1", "How do you feel?")
                      .WithAnswer("A", "Bad")
                      .Items.Last();

        Because of =
            () => Exception = Catch.Exception(() => Item.WithAnswer("A", "Bad again"));

        It should_give_a_nice_error = () => Exception.ShouldBeOfType<DuplicateAnswerException>();

        It should_give_a_nice_error_message =
            () => Exception.Message.ShouldEqual("The answer A was used twice in item HADS_1.");
    }
}