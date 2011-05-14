using System.Collections.Generic;
using System.Linq;

using AskMe.Model;

using Machine.Specifications;

namespace AskMe.TextParser.Specs
{
    public class when_parsing_a_coded_question : when_parsing
    {
        Establish context =
            () => Text = "HADS_1: How do you feel today?";

        It should_contain_one_question =
            () => Questions.Count.ShouldEqual(1);

        It should_contain_the_given_sentence =
            () => GetQuestion(0).Text.ShouldEqual("How do you feel today?");

        It should_have_the_given_question_code =
            () => GetQuestion(0).Code.ShouldEqual("HADS_1");
    }
}