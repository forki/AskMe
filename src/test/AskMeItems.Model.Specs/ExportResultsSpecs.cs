using System;

using AskMeItems.Model.Specs.Data;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_exporting_the_results_as_CSV : when_using_an_answered_questionaire_with_two_items
    {
        static string[] Text;

        Because of =
            () =>
            Text =
            Presenter.ExportAsCSV()
                .Split(new[] {"\r\n"}, StringSplitOptions.None);

        It should_format_the_first_answer = () => Text[0].ShouldEqual("HADS_1\tA\t1");
        It should_format_the_second_answer = () => Text[1].ShouldEqual("HADS_2\tB\t4");
    }
}