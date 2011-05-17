using System;
using System.Collections.Generic;
using System.Linq;

namespace AskMeItems.Model
{
    public class Questionnaire
    {
        readonly Dictionary<string, Item> _questions;

        public Questionnaire(IEnumerable<Item> questions)
        {
            Items = new List<Item>();
            _questions = new Dictionary<string, Item>();
            foreach (var question in questions)
            {
                AddQuestion(question);
            }
        }

        public List<Item> Items { get; private set; }

        void AddQuestion(Item item)
        {
            try
            {
                _questions.Add(item.Code, item);
            }
            catch (Exception ex)
            {
                throw new DuplicateItemException(item.Code, ex);
            }
            Items.Add(item);
        }

        public List<Subscale> GetSubscales()
        {
            var subscales = new Dictionary<string, List<Item>>();
            foreach (var question in _questions.Values)
            {
                if (question.Code.Contains("_"))
                {
                    AddToDict(question, subscales, question.Code.Split('_')[0]);
                }
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
            {
                subscales[subscaleCode] = new List<Item>();
            }
            subscales[subscaleCode].Add(question);
        }
    }
}