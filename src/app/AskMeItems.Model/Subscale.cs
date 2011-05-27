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
            Type = SubscaleType.Subscale;
            if (string.IsNullOrEmpty(name))
                Type = SubscaleType.Questionnaire;
        }

        public string Name { get; private set; }
        public List<Result> Results { get; private set; }

        public double Average
        {
            get { return Results.Select(x => x.SelectedAnswer.Points).Average(); }
        }

        public int Points
        {
            get { return Results.Select(x => x.SelectedAnswer.Points).Sum(); }
        }

        public SubscaleType Type { get; private set; }
    }
}