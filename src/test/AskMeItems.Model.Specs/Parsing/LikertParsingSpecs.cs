using Machine.Specifications;

namespace AskMeItems.Model.Specs.Parsing
{
    public class when_parsing_a_likert_questionnaire : when_parsing
    {
        Establish context =
            () => Text =
                  "Questionnaire-Type: Likert\r\n" +
                  "LIK_1: Ich fühle mich angespannt und überreizt.\r\n" +
                  "  1) sehr - 1\r\n" +
                  "  2) - 2\r\n" +
                  "  3) - 3\r\n" +
                  "  4)\r\n" +
                  "  5)\r\n" +
                  "  6) kaum - 6\r\n" +
                  "LIK_2: Ich fühle mich angespannt und überreizt.\r\n" +
                  "  1) sehr - 1\r\n" +
                  "  2) - 2\r\n" +
                  "  3) - 3\r\n" +
                  "  4) - 4\r\n" +
                  "  5) - 5\r\n" +
                  "  6) kaum - 6\r\n";

        It should_be_a_likert_questionnaire = () => Questionnaire.Type.ShouldEqual(QuestionnaireType.Likert);
        It should_contain_two_items = () => Questionnaire.Items.Count.ShouldEqual(2);
        It should_contain_the__LIK_1_item = () => GetItem(0).Code.ShouldEqual("LIK_1");
        It should_contain_the_text_for_the_first_answer = () => GetAnswer(0, 0).Text.ShouldEqual("sehr");
        It should_contain_no_text_for_the_second_answer = () => GetAnswer(0, 1).Text.ShouldEqual("");
        It should_contain_the_points_for_the_second_answer = () => GetAnswer(0, 1).Points.ShouldEqual(2);
        It should_contain_the_default_points_if_no_points_are_given = () => GetAnswer(0, 3).Points.ShouldEqual(4);
    }
}