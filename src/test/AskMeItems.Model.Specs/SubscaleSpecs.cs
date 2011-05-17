using System.Collections.Generic;
using System.Linq;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_finding_subscales : when_using_a_questionaire_with_two_items
    {
        static List<Subscale> Subscales;
        Because of = () => Subscales = Questionnaire.GetSubscales();

        It should_contain_2_scales =
            () => Subscales.Count.ShouldEqual(2);

        It should_contain_one_subscale =
            () => Subscales.Select(s => s.Name).ShouldContain("HADS");

        It should_contain_both_items_in_the_subscale =
            () => Subscales.Where(s => s.Name == "HADS").First().Items.Count.ShouldEqual(2);

        It should_contain_both_items_in_the_mainscale =
            () => Subscales.Where(s => s.Name == "").First().Items.Count.ShouldEqual(2);

        It should_contain_the_mainscale = 
            () => Subscales.Select(s => s.Name).ShouldContain("");
    }
}