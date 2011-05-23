using System;

using AskMeItems.Model.Specs.Data;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_exporting_the_results : when_using_a_questionaire_with_two_items
    {
        static QuestionnairePresenter Presenter;
        static string[] Text;

        Establish context =
            () =>
            Presenter =
            Questionnaire
                .ToPresenter()
                .AnswerWith("A")
                .AnswerWith("B");

        Because of =
            () =>
            Text =
            Presenter.Export()
                .Split(new[] {"\r\n"}, StringSplitOptions.None);

        It should_format_the_first_answer = () => Text[0].ShouldEqual("HADS_1\tA\t1");
        It should_format_the_second_answer = () => Text[1].ShouldEqual("HADS_2\tB\t4");
    }
}