using System.Linq;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_presenting_a_questionaire_with_two_items : when_using_a_questionaire_with_two_items
    {
        protected static QuestionnairePresenter Presenter;

        Establish context = () => Presenter = Questionnaire.ToPresenter();
    }

    public class when_asking_for_first_item : when_presenting_a_questionaire_with_two_items
    {
        static Item Item;

        Because of = () => Item = Presenter.CurrentItem;

        It should_give_the_item = () => Item.Code.ShouldEqual("HADS_1");
        It should_have_another_item = () => Presenter.HasItem().ShouldBeTrue();
    }

    public class when_asking_for_current_item_after_answering_an_item : when_presenting_a_questionaire_with_two_items
    {
        static Item Item;

        Because of =
            () =>
            {
                Presenter.AnswerCurrentItem(Presenter.CurrentItem.Answers.Values.First());
                Item = Presenter.CurrentItem;
            };

        It should_give_the_item = () => Item.Code.ShouldEqual("HADS_2");
        It should_have_another_item = () => Presenter.HasItem().ShouldBeTrue();
    }

    public class when_asking_for_current_item_after_answering_two_items : when_presenting_a_questionaire_with_two_items
    {
        Because of =
            () =>
            {
                Presenter.AnswerCurrentItem(Presenter.CurrentItem.Answers.Values.First());
                Presenter.AnswerCurrentItem(Presenter.CurrentItem.Answers.Values.First());
            };

        It should_not_have_another_item = () => Presenter.HasItem().ShouldBeFalse();
    }
}