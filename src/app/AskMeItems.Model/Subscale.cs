using System.Collections.Generic;

namespace AskMeItems.Model
{
    public class Subscale
    {
        public Subscale(string name, List<Item> items)
        {
            Name = name;
            Items = items;
        }

        public string Name { get; private set; }
        public List<Item> Items { get; private set; }
    }
}