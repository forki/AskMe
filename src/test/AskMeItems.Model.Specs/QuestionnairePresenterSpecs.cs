using System;
using System.Linq;

using AskMeItems.Model.Specs.Data;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_presenting_a_questionaire_with_two_items : when_using_a_questionaire_with_two_items
    {
        protected static QuestionnairePresenter Presenter;

        Establish context = () => Presenter = Questionnaire.ToPresenter("1");

        public class when_asking_for_first_item
        {
            static Item Item;

            Because of = () => Item = Presenter.CurrentItem;

            It should_give_the_item = () => Item.Code.ShouldEqual("HADS_1");
            It should_not_have_a_result = () => Presenter.Results.ShouldBeEmpty();
            It should_have_another_item = () => Presenter.HasItem().ShouldBeTrue();
            It should_not_have_a_introduction = () => Presenter.HasIntroduction.ShouldBeFalse();
        }

        public class when_answering_an_item
        {
            static Answer GivenAnswer;

            Because of =
                () =>
                {
                    GivenAnswer = Presenter.CurrentItem.Answers.Values.First();
                    Presenter.AnswerCurrentItem(GivenAnswer);
                };

            It should_have_one_item_answered = () => Presenter.Results.Count.ShouldEqual(1);

            It should_have_recorded_the_given_answer =
                () => Presenter.Results.First().SelectedAnswer.ShouldEqual(GivenAnswer);

            It should_have_another_item = () => Presenter.HasItem().ShouldBeTrue();
        }

        public class when_answering_an_item_with_an_invalid_answer
        {
            static Exception Exception;

            Because of =
                () => Exception = Catch.Exception(() => Presenter.AnswerCurrentItem(new Answer("Test", "foo", 0)));

            It should_report_that_the_answer_is_not_allowed =
                () => Exception.ShouldBeOfType<AnswerNotAllowedException>();
        }

        public class when_answering_an_item_with_nothing
        {
            static Exception Exception;

            Because of =
                () => Exception = Catch.Exception(() => Presenter.AnswerCurrentItem(null));

            It should_report_that_the_answer_is_not_allowed =
                () => Exception.ShouldBeOfType<AnswerNotAllowedException>();

            It should_report_a_nice_error_text =
                () => Exception.Message.ShouldNotBeEmpty();

            It should_report_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
        }

        public class when_asking_for_current_item_after_answering_an_item
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

        public class when_answering_two_items
        {
            static Answer LastGivenAnswer;

            Because of =
                () =>
                {
                    Presenter.AnswerCurrentItem(Presenter.CurrentItem.Answers.Values.First());
                    LastGivenAnswer = Presenter.CurrentItem.Answers.Values.Skip(1).First();
                    Presenter.AnswerCurrentItem(LastGivenAnswer);
                };

            It should_have_two_items_answered = () => Presenter.Results.Count.ShouldEqual(2);

            It should_have_recorded_the_given_answer =
                () => Presenter.Results.Last().SelectedAnswer.ShouldEqual(LastGivenAnswer);

            It should_not_have_another_item = () => Presenter.HasItem().ShouldBeFalse();
        }
    }
}