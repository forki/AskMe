using System;
using System.Collections.Generic;

namespace AskMe.Model
{
    public class Questionaire
    {
        readonly Dictionary<string, Item> _questions;

        public Questionaire(IEnumerable<Item> questions)
        {
            Items = new List<Item>();
            _questions = new Dictionary<string, Item>();
            foreach (var question in questions)
                AddQuestion(question);
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
    }
}