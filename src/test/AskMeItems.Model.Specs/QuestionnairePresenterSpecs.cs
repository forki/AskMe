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
        It should_not_have_an_answer = () => Presenter.Answers.ShouldBeEmpty();
        It should_have_another_item = () => Presenter.HasItem().ShouldBeTrue();
    }

    public class when_answering_an_item : when_presenting_a_questionaire_with_two_items
    {
        static Answer GivenAnswer;

        Because of =
            () =>
            {
                GivenAnswer = Presenter.CurrentItem.Answers.Values.First();
                Presenter.AnswerCurrentItem(GivenAnswer);
            };

        It should_have_one_item_answered = () => Presenter.Answers.Count.ShouldEqual(1);
        It should_have_recorded_the_given_answer = () => Presenter.Answers.First().ShouldEqual(GivenAnswer);
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

    public class when_answering_two_items : when_presenting_a_questionaire_with_two_items
    {
        static Answer LastGivenAnswer;

        Because of =
            () =>
            {
                Presenter.AnswerCurrentItem(Presenter.CurrentItem.Answers.Values.First());
                LastGivenAnswer = Presenter.CurrentItem.Answers.Values.First();
                Presenter.AnswerCurrentItem(LastGivenAnswer);
            };

        It should_have_two_items_answered = () => Presenter.Answers.Count.ShouldEqual(2);
        It should_have_recorded_the_given_answer = () => Presenter.Answers.Last().ShouldEqual(LastGivenAnswer);
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