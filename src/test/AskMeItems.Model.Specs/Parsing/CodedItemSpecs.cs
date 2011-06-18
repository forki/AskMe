using Machine.Specifications;

namespace AskMeItems.Model.Specs.Parsing
{
    public class when_parsing_a_coded_question : when_parsing
    {
        Establish context =
            () => Text = "HADS_1: How do you feel today?";

        It should_contain_one_question =
            () => Questionnaire.Items.Count.ShouldEqual(1);

        It should_contain_the_given_sentence =
            () => GetItem(0).Text.ShouldEqual("How do you feel today?");

        It should_have_the_given_question_code =
            () => GetItem(0).Code.ShouldEqual("HADS_1");
    }

    public class when_parsing_a_coded_question_with_dash : when_parsing
    {
        Establish context =
            () => Text = "ADS-K_1:Mich haben Dinge beunruhigt, die mir sonst nichts ausmachen.";

        It should_contain_one_question =
            () => Questionnaire.Items.Count.ShouldEqual(1);

        It should_contain_the_given_sentence =
            () => GetItem(0).Text.ShouldEqual("Mich haben Dinge beunruhigt, die mir sonst nichts ausmachen.");

        It should_have_the_given_question_code =
            () => GetItem(0).Code.ShouldEqual("ADS-K_1");
    }
}