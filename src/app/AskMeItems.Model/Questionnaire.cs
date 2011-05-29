using System;
using System.Collections.Generic;

namespace AskMeItems.Model
{
    public class Questionnaire
    {
        readonly Dictionary<string, Item> _items;

        public Questionnaire(QuestionnaireType type, string instruction, IEnumerable<Item> questions)
        {
            Type = type;
            Instruction = instruction;
            Items = new List<Item>();
            _items = new Dictionary<string, Item>();
            foreach (var question in questions)
                AddQuestion(question);
        }

        public List<Item> Items { get; private set; }

        public QuestionnaireType Type { get; private set; }

        public string Instruction { get; private set; }

        void AddQuestion(Item item)
        {
            try
            {
                _items.Add(item.Code, item);
            }
            catch (Exception ex)
            {
                throw new DuplicateItemException(item.Code, ex);
            }
            Items.Add(item);
        }
    }
}