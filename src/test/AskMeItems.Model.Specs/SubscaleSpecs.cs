using System.Collections.Generic;
using System.Linq;

using AskMeItems.Model.Specs.Data;

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

    public class when_looking_for_subscales : when_using_a_questionaire_with_three_subscales
    {
        Because of = () => Subscales = Questionnaire.GetSubscales();

        It should_contain_4_scales =
            () => Subscales.Count.ShouldEqual(4);

        It should_contain_three_subscale =
            () => Subscales.Where(s => s.Name != "").Count().ShouldEqual(3);

        It should_contain_both_items_in_the_A_subscale =
            () => Subscales.Where(s => s.Name == "A").First().Items.Count.ShouldEqual(2);

        It should_contain_three_items_in_the_B_subscale =
            () => Subscales.Where(s => s.Name == "B").First().Items.Count.ShouldEqual(3);

        It should_contain_one_item_in_the_C_subscale =
            () => Subscales.Where(s => s.Name == "C").First().Items.Count.ShouldEqual(1);

        It should_contain_all_items_in_the_mainscale =
            () => Subscales.Where(s => s.Name == "").First().Items.Count.ShouldEqual(6);

        It should_contain_the_mainscale =
            () => Subscales.Select(s => s.Name).ShouldContain("");
    }
}