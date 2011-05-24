using System.Collections.Generic;
using System.Linq;

namespace AskMeItems.Model
{
    class SubscaleAnalyzer
    {
        public static List<Subscale> GetSubscalesFor(List<Result> results)
        {
            var subscales = new Dictionary<string, List<Result>>();
            foreach (var result in results)
            {
                if (result.Item.Code.Contains("_"))
                    AddToDict(result, subscales, result.Item.Code.Split('_')[0]);
                AddToDict(result, subscales, "");
            }

            return
                subscales
                    .Select(subscale => new Subscale(subscale.Key, subscale.Value))
                    .OrderByDescending(subscale => subscale.Name)
                    .ToList();
        }

        static void AddToDict(Result result, IDictionary<string, List<Result>> subscales, string subscaleCode)
        {
            if (!subscales.ContainsKey(subscaleCode))
                subscales[subscaleCode] = new List<Result>();
            subscales[subscaleCode].Add(result);
        }
    }
}