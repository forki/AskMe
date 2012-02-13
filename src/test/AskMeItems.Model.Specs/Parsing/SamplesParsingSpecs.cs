using System.Linq;

using Machine.Specifications;

namespace AskMeItems.Model.Specs.Parsing
{
    public class when_parsing_a_likert_questionnaire : when_parsing_from_file
    {
        Establish context = () => FileName = @"Data\Likert.txt";

        It should_use_the_filename_as_questionnaire_code = () => Questionnaire.Code.ShouldEqual("Likert");
        It should_be_a_likert_questionnaire = () => Questionnaire.Type.ShouldEqual(QuestionnaireType.Likert);
        It should_contain_two_items = () => Questionnaire.Items.Count.ShouldEqual(3);
        It should_contain_the_LIK_1_item = () => GetItem(0).Code.ShouldEqual("LIK_1");
        It should_contain_the_text_for_the_first_answer = () => GetAnswer(0, 0).Text.ShouldEqual("sehr");
        It should_contain_no_text_for_the_second_answer = () => GetAnswer(0, 1).Text.ShouldEqual("");
        It should_contain_the_points_for_the_second_answer = () => GetAnswer(0, 1).Points.ShouldEqual(2);
        It should_contain_the_default_points_if_no_points_are_given = () => GetAnswer(0, 3).Points.ShouldEqual(4);

        It should_contain_the_LIK3_item = () => GetItem(2).Code.ShouldEqual("LIK_3");
        It should_contain_a_linebreak_in_the_LIK3_answer = () => GetItem(2).Answers["10"].Text.ShouldEqual("very bad\r\nreally bad");
    }

    public class when_parsing_a_ADS_questionnaire : when_parsing_from_file
    {
        Establish context = () => FileName = @"Data\ADS-K.txt";

        It should_use_the_filename_as_questionnaire_code = () => Questionnaire.Code.ShouldEqual("ADS-K");
        It should_be_a_likert_questionnaire = () => Questionnaire.Type.ShouldEqual(QuestionnaireType.Likert);
        It should_contain_two_items = () => Questionnaire.Items.Count.ShouldEqual(3);

        It should_contain_the_sample_in_the_instruction =
            () => Questionnaire.Instruction.ShouldContain("3 - meistens, die ganze Zeit (5 bis 7 Tage lang)");

        It should_contain_the_7_lines_in_the_instruction =
            () => Questionnaire.Instruction.Split('\n').Length.ShouldEqual(7);

        It should_contain_the_ADSK_item = () => GetItem(0).Code.ShouldEqual("ADS-K_1");

        It should_have_the_first_item_marked_as_excluded_from_subscales =
            () => GetItem(0).ExcludeFromSubscales.ShouldBeTrue();

        It should_contain_the_text_for_the_first_answer = () => GetAnswer(0, 0).Text.ShouldEqual("selten");
        It should_contain_the_points_for_the_first_answer = () => GetAnswer(0, 0).Points.ShouldEqual(0);
    }

    public class when_parsing_a_PSQ_questionnaire : when_parsing_from_file
    {
        Establish context = () => FileName = @"Data\PSQ.txt";

        It should_be_a_likert_questionnaire = () => Questionnaire.Type.ShouldEqual(QuestionnaireType.Likert);
        It should_contain_two_items = () => Questionnaire.Items.Count.ShouldEqual(3);
    }

    public class when_parsing_a_HADS_questionnaire : when_parsing_from_file
    {
        Establish context = () => FileName = @"Data\HADS.txt";

        It should_be_a_questionnaire_with_listed_answers =
            () => Questionnaire.Type.ShouldEqual(QuestionnaireType.ListedAnswers);
    }
}