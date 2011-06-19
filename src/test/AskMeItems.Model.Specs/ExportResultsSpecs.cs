using AskMeItems.Model.Export;
using AskMeItems.Model.Specs.Data;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_exporting_the_results_as_CSV_with_subscales : when_using_an_answered_questionaire_with_two_items
    {
        static string[] Text;

        Because of =
            () =>
            Text = Presenter
                       .Export(new CSVExporterWithSubscales())
                       .SplitOnLineBreaks();

        It should_format_the_first_answer = () => Text[0].ShouldEqual("ITEM\tHADS_1\tA\t1");
        It should_format_the_second_answer = () => Text[1].ShouldEqual("ITEM\tHADS_2\tB\t4");
        It should_format_the_subscale = () => Text[2].ShouldEqual("SUBSCALE\tHADS\t5\t2.5");
        It should_format_the_mainscale = () => Text[3].ShouldEqual("QUESTIONNAIRE\t\t5\t2.5");
    }

    public class when_exporting_the_results_as_CSV : when_using_an_answered_questionaire_with_two_items
    {
        static string[] Text;

        Because of =
            () =>
            Text = Presenter
                       .Export(new CSVExporter())
                       .TrimLineBreaksAtEnd()
                       .SplitOnLineBreaks();

        It should_format_the_first_answer = () => Text[0].ShouldEqual("ITEM\tHADS_1\tA\t1");
        It should_format_the_second_answer = () => Text[1].ShouldEqual("ITEM\tHADS_2\tB\t4");
        It should_have_no_subscales = () => Text.Length.ShouldEqual(2);
    }

    public class when_generating_filename_with_subscales : when_using_an_answered_questionaire_with_two_items
    {
        static string FileName;

        Because of =
            () => FileName = Presenter.GenerateFileName(new CSVExporterWithSubscales());

        It should_generate_the_filename_with_the_questionnaire_and_subject_code = 
            () => FileName.ShouldEqual("ResultWithSubscales_HADS_1.txt");
    }

    public class when_generating_filename : when_using_an_answered_questionaire_with_two_items
    {
        static string FileName;

        Because of =
            () => FileName = Presenter.GenerateFileName(new CSVExporter());

        It should_generate_the_filename_with_the_questionnaire_and_subject_code = 
            () => FileName.ShouldEqual("Result_HADS_1.txt");
    }


}