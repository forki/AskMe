using System.Collections.Generic;
using System.Linq;

namespace AskMeItems.Model
{
    public class Subscale
    {
        internal Subscale(string name, List<Result> results)
        {
            Name = name;
            Results = results;
        }

        public string Name { get; private set; }
        public List<Result> Results { get; private set; }

        public double Average
        {
            get { return Results.Select(x => x.Points).Average(); }
        }
    }
}