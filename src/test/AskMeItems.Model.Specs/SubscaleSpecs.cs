using System.Collections.Generic;
using System.Linq;

using AskMeItems.Model.Specs.Data;

using Machine.Specifications;

namespace AskMeItems.Model.Specs
{
    public class when_finding_subscales : when_using_an_answered_questionaire_with_two_items
    {
        static List<Subscale> Subscales;
        Because of = () => Subscales = Presenter.GetSubscales();

        It should_contain_2_scales =
            () => Subscales.Count.ShouldEqual(2);

        It should_contain_one_subscale =
            () => Subscales.Where(s => s.Name == "HADS").ShouldNotBeEmpty();

        It should_contain_both_items_in_the_subscale =
            () => Subscales.ByName("HADS").Results.Count.ShouldEqual(2);

        It should_contain_both_items_in_the_mainscale =
            () => Subscales.ByName("").Results.Count.ShouldEqual(2);

        It should_contain_the_mainscale =
            () => Subscales.Where(s => s.Name == "").ShouldNotBeEmpty();
    }

    public class when_looking_for_subscales : when_using_an_answered_questionaire_with_three_subscales
    {
        static List<Subscale> Subscales;
        Because of = () => Subscales = Presenter.GetSubscales();

        It should_contain_4_scales =
            () => Subscales.Count.ShouldEqual(4);

        It should_contain_three_subscale =
            () => Subscales.Where(s => s.Name != "").Count().ShouldEqual(3);

        It should_contain_both_items_in_the_A_subscale =
            () => Subscales.ByName("A").Results.Count.ShouldEqual(2);

        It should_contain_three_items_in_the_B_subscale =
            () => Subscales.ByName("B").Results.Count.ShouldEqual(3);

        It should_contain_one_item_in_the_C_subscale =
            () => Subscales.ByName("C").Results.Count.ShouldEqual(1);

        It should_contain_all_items_in_the_mainscale =
            () => Subscales.ByName("").Results.Count.ShouldEqual(6);

        It should_contain_the_mainscale =
            () => Subscales.Where(s => s.Name == "").ShouldNotBeEmpty();
    }

    public class when_calculating_averages : when_using_an_answered_questionaire_with_three_subscales
    {
        static List<Subscale> Subscales;
        Because of = () => Subscales = Presenter.GetSubscales();

        It should_have_the_average_for_the_main_scale =
            () => Subscales.ByName("").Average.ShouldBeCloseTo(2.83333333333333);

        It should_have_the_average_for_the_B_subscale =
            () => Subscales.ByName("B").Average.ShouldBeCloseTo(2);
    }
}