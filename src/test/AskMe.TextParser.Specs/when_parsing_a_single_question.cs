using System.Collections.Generic;
using System.Linq;
using AskMe.Model;
using Machine.Specifications;

namespace AskMe.TextParser.Specs
{
    public class when_parsing_a_single_question
    {
        private static string Text = "Ich fühle mich angespannt und überreizt.";
        private static Parser Parser;
        private static List<Question> Questions;

        Establish context = () => Parser = new Parser();
        Because of = () => Questions = Parser.Parse(Text);
        It should_contain_one_question = () => Questions.Count.ShouldEqual(1);
        It should_contain_the_given_sentence = () => Questions.First().Text.ShouldEqual(Text);
    }
}