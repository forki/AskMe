using AskMeItems.Model.Specs.Data;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_exporting_the_results_as_CSV : when_using_an_answered_questionaire_with_two_items
    {
        static string[] Text;

        Because of =
            () =>
            Text = Presenter
                       .Export()
                       .SplitOnLineBreaks();

        It should_format_the_first_answer = () => Text[0].ShouldEqual("ITEM\tHADS_1\tA\t1");
        It should_format_the_second_answer = () => Text[1].ShouldEqual("ITEM\tHADS_2\tB\t4");
        It should_format_the_subscale = () => Text[2].ShouldEqual("SUBSCALE\tHADS\t5\t2.5");
        It should_format_the_mainscale = () => Text[3].ShouldEqual("QUESTIONNAIRE\t\t5\t2.5");
    }
}