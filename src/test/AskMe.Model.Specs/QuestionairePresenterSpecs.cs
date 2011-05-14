using Machine.Specifications;

namespace AskMe.Model.Specs
{
    public class when_presenting_a_questionaire_with_two_items
    {
        protected static QuestionairePresenter Presenter;

        Establish context =
            () => Presenter =
                  Ask.Item("HADS_1", "How do you feel?")
                      .WithAnswer("A", "good")
                      .WithAnswer("B", "bad")
                      .Item("HADS_2", "How do you really feel?")
                      .WithAnswer("A", "very good")
                      .WithAnswer("B", "very bad")
                      .ToPresenter();
    }

    public class when_asking_for_first_item : when_presenting_a_questionaire_with_two_items
    {
        static Item Item;

        Because of = () => Item = Presenter.AskNextItem();

        It should_give_the_item = () => Item.Code.ShouldEqual("HADS_1");
        It should_have_another_item = () => Presenter.HasNextItem().ShouldBeTrue();
    }

    public class when_asking_for_second_item : when_presenting_a_questionaire_with_two_items
    {
        static Item Item;

        Because of =
            () =>
            {
                Presenter.AskNextItem();
                Item = Presenter.AskNextItem();
            };

        It should_give_the_item = () => Item.Code.ShouldEqual("HADS_2");
        It should_not_have_another_item = () => Presenter.HasNextItem().ShouldBeFalse();
    }
}