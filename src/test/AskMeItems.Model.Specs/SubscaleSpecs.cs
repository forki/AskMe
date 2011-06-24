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

        It should_contain_the_mainscale_with_rigth_type =
            () => Subscales.ByName("").Type.ShouldEqual(SubscaleType.Questionnaire);

        It should_contain_the_subscale_with_rigth_type =
            () => Subscales.ByName("HADS").Type.ShouldEqual(SubscaleType.Subscale);
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
            () => Subscales.ByName("B").Average.ShouldEqual(2);

        It should_have_the_points_for_the_B_subscale =
            () => Subscales.ByName("A").Points.ShouldEqual(3);
    }

    public class when_calculating_averages_with_excluded_items
    {
        protected static QuestionnairePresenter Presenter;
        static List<Subscale> Subscales;

        Establish context =
            () =>
            {
                var Questionnaire =
                    Ask.NewQuestionnaire("TEST")
                        .Item("A_1", "How do you feel?")
                        .WithAnswer("A", "good", 1)
                        .WithAnswer("B", "bad", 2)
                        .Item("A_2", "How do you really feel?", true)
                        .WithAnswer("A", "very good", 1)
                        .WithAnswer("B", "very bad", 2);

                Presenter =
                    Questionnaire
                        .ToPresenter("1")
                        .AnswerWith("B")
                        .AnswerWith("A");
            };

        Because of = () => Subscales = Presenter.GetSubscales();

        It should_have_the_average_for_the_main_scale =
            () => Subscales.ByName("").Average.ShouldBeCloseTo(2);

        It should_have_the_points_for_the_B_subscale =
            () => Subscales.ByName("A").Points.ShouldEqual(2);
    }
}