using System;
using System.Linq;

using AskMeItems.Model.Specs.Data;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_creating_an_item
    {
        static Item Item;

        Because of =
            () => Item =
                  Ask.NewQuestionnaire("HADS")
                      .Item("HADS_1", "How do you feel?")
                      .Items.Last();

        It should_have_nice_description = () => Item.ToString().ShouldEqual("HADS_1: How do you feel?");
    }

    public class when_adding_two_answers_with_same_code
    {
        static Item Item;
        static Exception Exception;

        Establish context =
            () => Item =
                  Ask.NewQuestionnaire("HADS")
                      .Item("HADS_1", "How do you feel?")
                      .WithAnswer("A", "Bad", 0)
                      .Items.Last();

        Because of =
            () => Exception = Catch.Exception(() => Item.WithAnswer("A", "Bad again", 0));

        It should_give_a_nice_error = () => Exception.ShouldBeOfType<DuplicateAnswerException>();

        It should_give_a_nice_error_message =
            () => Exception.Message.ShouldEqual("The answer A was used twice in item HADS_1.");
    }
}