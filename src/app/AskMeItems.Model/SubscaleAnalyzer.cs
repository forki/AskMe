using System.Collections.Generic;
using System.Linq;

namespace AskMeItems.Model
{
    public class SubscaleAnalyzer
    {
        public static List<Subscale> GetSubscalesFor(Questionnaire questionnaire)
        {
            var subscales = new Dictionary<string, List<Item>>();
            foreach (var question in questionnaire.Items)
            {
                if (question.Code.Contains("_"))
                    AddToDict(question, subscales, question.Code.Split('_')[0]);
                AddToDict(question, subscales, "");
            }

            return
                subscales
                    .Select(subscale => new Subscale(subscale.Key, subscale.Value))
                    .ToList();
        }

        static void AddToDict(Item question, IDictionary<string, List<Item>> subscales, string subscaleCode)
        {
            if (!subscales.ContainsKey(subscaleCode))
                subscales[subscaleCode] = new List<Item>();
            subscales[subscaleCode].Add(question);
        }
    }
}