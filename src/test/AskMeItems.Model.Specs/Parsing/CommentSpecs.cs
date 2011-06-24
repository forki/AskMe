using Machine.Specifications;

namespace AskMeItems.Model.Specs.Parsing
{
    public class when_parsing_a_questionnaire_with_comments : when_parsing
    {
        Establish context =
            () => Text = "#Questionnaire-Type: Likert\r\n" +
                         "Instruction: In this questionnaire you have to answer as fast as possible.\r\n" +
                         "#LIK_2: I'm feeling good.\r\n" +
                         "#  1) yes - 1\r\n" +
                         "#  2) a little bit\r\n" +
                         "#  3) not at all\r\n" +
                         "LIK_1: I'm feeling good.\r\n" +
                         "  1) yes - 1\r\n" +
                         "  2) a little bit\r\n" +
                         "  3) not at all";

        It should_contain_one_question =
            () => Questionnaire.Items.Count.ShouldEqual(1);

        It should_not_recognize_the_likert_type =
            () => Questionnaire.Type.ShouldEqual(QuestionnaireType.ListedAnswers);
    }
}