using System.Collections.Generic;

using Machine.Specifications;

namespace AskMeItems.Model.Specs.Parsing
{
    public class when_parsing_a_questionnaire_with_intro : when_parsing
    {
        Establish context =
            () => Text = "Questionnaire-Type: Likert\r\n" +
                         "Instruction: In this questionnaire you have to answer as fast as possible.\r\n" +
                         "\r\n" +
                         "  There are no right or wrong answers.\r\n" +
                         "LIK_1: I'm feeling good.\r\n" +
                         "  1) yes - 1\r\n" +
                         "  2) a little bit\r\n" +
                         "  3) not at all";

        It should_contain_one_question =
            () => Questionnaire.Items.Count.ShouldEqual(1);

        It should_have_parsed_the_intro_text =
            () => Questionnaire.Instruction
                      .ShouldEqual("In this questionnaire you have to answer as fast as possible.\r\n" +
                                   "\r\n" +
                                   "There are no right or wrong answers.");

        It should_contain_the_given_sentence =
            () => GetItem(0).Text.ShouldEqual("I'm feeling good.");
    }

    public class when_presenting_a_questionnaire_with_intro
    {
        static QuestionnairePresenter Presenter;

        static Questionnaire Questionnaire;

        Establish context =
            () => Questionnaire = new Questionnaire("WithIntro", QuestionnaireType.Likert, "blablub", new List<Item>());

        Because of =
            () => Presenter = new QuestionnairePresenter(null, "1", Questionnaire);

        It should_have_a_instruction = () => Presenter.HasIntroduction.ShouldBeTrue();
        It should_have_the_insturction_text = () => Questionnaire.Instruction.ShouldEqual("blablub");
    }
}