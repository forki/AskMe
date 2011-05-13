using System.Collections.Generic;
using System.Linq;
using AskMe.Model;
using Machine.Specifications;

namespace AskMe.TextParser.Specs
{
    public class when_parsing
    {
        protected static string Text;
        private static readonly QuestionaireParser QuestionaireParser = new QuestionaireParser();
        protected static List<Question> Questions;
        private Because of = () => Questions = QuestionaireParser.Parse(Text);

        public static Answer GetAnswer(int question, int answer)
        {
            return GetQuestion(question).Answers.Values.Skip(answer).First();
        }

        public static Question GetQuestion(int question)
        {
            return Questions.Skip(question).First();
        }
    }

    public class when_parsing_a_single_question : when_parsing
    {
        private Establish context =
            () => Text = "Ich fühle mich angespannt und überreizt.";

        private It should_contain_one_question =
            () => Questions.Count.ShouldEqual(1);

        private It should_contain_the_given_sentence =
            () => GetQuestion(0).Text.ShouldEqual(Text);
    }

    public class when_parsing_a_single_question_with_two_answers : when_parsing
    {
        private Establish context =
            () => Text =
                  "Ich fühle mich angespannt und überreizt.\r\n" +
                  "  A) oft\r\n" +
                  "  B) von Zeit zu Zeit, gelegentlich";

        private It should_contain_one_question =
            () => Questions.Count.ShouldEqual(1);

        private It should_contain_the_given_answer_code_for_A =
            () => GetAnswer(0, 0).Code.ShouldEqual("A");

        private It should_contain_the_given_answer_code_for_B =
            () => GetAnswer(0, 1).Code.ShouldEqual("B");

        private It should_contain_the_given_answer_text_for_A =
            () => GetAnswer(0, 0).Text.ShouldEqual("oft");

        private It should_contain_the_given_answer_text_for_B =
            () => GetAnswer(0, 1).Text.ShouldEqual("von Zeit zu Zeit, gelegentlich");

        private It should_contain_the_given_question =
            () => GetQuestion(0).Text.ShouldEqual("Ich fühle mich angespannt und überreizt.");

        private It should_contain_two_answers =
            () => GetQuestion(0).Answers.Count.ShouldEqual(2);
    }


    public class when_parsing_two_questions : when_parsing
    {
        private Establish context =
            () => Text =
                  "Ich fühle mich angespannt und überreizt.\r\n" +
                  "  A) oft\r\n" +
                  "  B) von Zeit zu Zeit, gelegentlich\r\n" +
                  "Mich überkommt eine ängstliche Vorahnung,dass etwas Schreckliches passieren könnte.\r\n" +
                  "  A) ja, sehr stark\r\n" +
                  "  B) ja, aber nicht allzu stark\r\n" +
                  "  C) etwas, aber es macht mir keine Sorgen\r\n" +
                  "  D) überhaupt nicht";

        private It should_contain_two_questions =
            () => Questions.Count.ShouldEqual(2);

        private It should_contain_the_fours_answers_for_the_second_question =
            () => GetQuestion(1).Answers.Count.ShouldEqual(4);

        private It should_contain_the_second_given_question =
            () => GetQuestion(1).Text.ShouldEqual("Mich überkommt eine ängstliche Vorahnung,dass etwas Schreckliches passieren könnte.");
    }

    public class when_parsing_two_questions_with_linux_line_endings : when_parsing
    {
        private Establish context =
            () => Text =
                  "Ich fühle mich angespannt und überreizt.\n" +
                  "  A) oft\n" +
                  "  B) von Zeit zu Zeit, gelegentlich\n" +
                  "Mich überkommt eine ängstliche Vorahnung,dass etwas Schreckliches passieren könnte.\n" +
                  "  A) ja, sehr stark\n" +
                  "  B) ja, aber nicht allzu stark\n" +
                  "  C) etwas, aber es macht mir keine Sorgen\n" +
                  "  D) überhaupt nicht";

        private It should_contain_two_questions =
            () => Questions.Count.ShouldEqual(2);

        private It should_contain_the_fours_answers_for_the_second_question =
            () => GetQuestion(1).Answers.Count.ShouldEqual(4);
    }
}