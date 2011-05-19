namespace AskMeItems.TextParser.Specs
{
    public class when_parsing_a_single_question : when_parsing
    {
        Establish context =
            () => Text = "Ich fühle mich angespannt und überreizt.";

        It should_contain_one_question =
            () => Questionnaire.Items.Count.ShouldEqual(1);

        It should_contain_the_given_sentence =
            () => GetItem(0).Text.ShouldEqual(Text);
    }

    public class when_parsing_a_single_question_with_two_answers : when_parsing
    {
        Establish context =
            () => Text =
                  "Ich fühle mich angespannt und überreizt.\r\n" +
                  "  A) oft\r\n" +
                  "  B) von Zeit zu Zeit, gelegentlich";

        It should_contain_one_question =
            () => Questionnaire.Items.Count.ShouldEqual(1);

        It should_contain_the_given_answer_code_for_A =
            () => GetAnswer(0, 0).Code.ShouldEqual("A");

        It should_contain_the_given_answer_code_for_B =
            () => GetAnswer(0, 1).Code.ShouldEqual("B");

        It should_contain_the_given_answer_text_for_A =
            () => GetAnswer(0, 0).Text.ShouldEqual("oft");

        It should_contain_the_given_answer_text_for_B =
            () => GetAnswer(0, 1).Text.ShouldEqual("von Zeit zu Zeit, gelegentlich");

        It should_contain_the_given_question =
            () => GetItem(0).Text.ShouldEqual("Ich fühle mich angespannt und überreizt.");

        It should_contain_two_answers =
            () => GetItem(0).Answers.Count.ShouldEqual(2);
    }

    public class when_parsing_a_question_with_pointed_answers : when_parsing
    {
        Establish context =
            () => Text =
                  "Ich fühle mich angespannt und überreizt.\r\n" +
                  "  A) oft - 2\r\n" +
                  "  B) von Zeit zu Zeit, gelegentlich - 3";

        It should_contain_the_given_answer_points_for_A =
            () => GetAnswer(0, 0).Points.ShouldEqual(2);

        It should_contain_the_given_answer_points_for_B =
            () => GetAnswer(0, 1).Points.ShouldEqual(3);
    }

    public class when_parsing_two_questions : when_parsing
    {
        Establish context =
            () => Text =
                  "Ich fühle mich angespannt und überreizt.\r\n" +
                  "  A) oft\r\n" +
                  "  B) von Zeit zu Zeit, gelegentlich\r\n" +
                  "Mich überkommt eine ängstliche Vorahnung, dass etwas Schreckliches passieren könnte.\r\n" +
                  "  A) ja, sehr stark\r\n" +
                  "  B) ja, aber nicht allzu stark\r\n" +
                  "  C) etwas, aber es macht mir keine Sorgen\r\n" +
                  "  D) überhaupt nicht";

        It should_contain_two_questions =
            () => Questionnaire.Items.Count.ShouldEqual(2);

        It should_contain_the_fours_answers_for_the_second_question =
            () => GetItem(1).Answers.Count.ShouldEqual(4);

        It should_contain_the_second_given_question =
            () => GetItem(1).Text.ShouldEqual(ExpectedQuestion);

        const string ExpectedQuestion =
            "Mich überkommt eine ängstliche Vorahnung, dass etwas Schreckliches passieren könnte.";
    }

    public class when_parsing_two_questions_with_linux_line_endings : when_parsing
    {
        Establish context =
            () => Text =
                  "Ich fühle mich angespannt und überreizt.\n" +
                  "  A) oft\n" +
                  "  B) von Zeit zu Zeit, gelegentlich\n" +
                  "Mich überkommt eine ängstliche Vorahnung, dass etwas Schreckliches passieren könnte.\n" +
                  "  A) ja, sehr stark\n" +
                  "  B) ja, aber nicht allzu stark\n" +
                  "  C) etwas, aber es macht mir keine Sorgen\n" +
                  "  D) überhaupt nicht";

        It should_contain_two_questions =
            () => Questionnaire.Items.Count.ShouldEqual(2);

        It should_contain_the_fours_answers_for_the_second_question =
            () => GetItem(1).Answers.Count.ShouldEqual(4);
    }
}