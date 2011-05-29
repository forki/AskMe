using Machine.Specifications;

namespace AskMeItems.Model.Specs.Parsing
{
    public class when_parsing_a_questionnaire_with_intro : when_parsing
    {
        Establish context =
            () => Text = "Questionnaire-Type: Likert\r\n" +
                         "Instruction: In this questionnaire you have to answer on a Likert-scale as fast as possible.\r\n" +
                         "  There are no right or wrong answers.\r\n" +
                         "LIK_1: I'm feeling good.\r\n" +
                         "  1) yes - 1\r\n" +
                         "  2) a little bit\r\n" +
                         "  3) not at all";

        It should_contain_one_question =
            () => Questionnaire.Items.Count.ShouldEqual(1);

        It should_have_parsed_the_intro_text =
            () => Questionnaire.Instruction.ShouldEqual("In this questionnaire you have to answer on a Likert-scale as fast as possible.\r\nThere are no right or wrong answers.");

        It should_contain_the_given_sentence =
            () => GetItem(0).Text.ShouldEqual("I'm feeling good.");
    }
}