using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AskMeItems.Model
{
    public class QuestionnairePresenter
    {
        public QuestionnairePresenter(Questionnaire questionnaire)
        {
            Questionnaire = questionnaire;
            Results = new List<Result>();
        }

        public Questionnaire Questionnaire { get; private set; }

        public Item CurrentItem
        {
            get { return Questionnaire.Items[Results.Count]; }
        }

        public List<Result> Results { get; private set; }

        public List<Subscale> GetSubscales()
        {
            return SubscaleAnalyzer.GetSubscalesFor(Results);
        }

        public void AnswerCurrentItem(Answer answer)
        {
            if (answer == null)
                throw new AnswerNotAllowedException(CurrentItem);

            Results.Add(CurrentItem.AnswerWith(answer.Code));
        }

        public bool HasItem()
        {
            return Results.Count < Questionnaire.Items.Count;
        }

        public override string ToString()
        {
            return string.Format("Answered: {0} - {1}", Results.Count, CurrentItem);
        }

        public string ExportAsCSV()
        {
            const string FieldDelimiter = "\t";
            const string LineDelimiter = "\r\n";
            return
                ExportResultsAsCSV(FieldDelimiter, LineDelimiter) +
                ExportSubscalesAsCSV(FieldDelimiter, LineDelimiter);
        }

        string ExportResultsAsCSV(string fieldDelimiter, string lineDelimiter)
        {
            return
                Results
                    .Select(x => string.Format("ITEM{1}{0}{1}{2}{1}{3}{4}",
                                               x.Item.Code,
                                               fieldDelimiter,
                                               x.SelectedAnswer.Code,
                                               x.Points,
                                               lineDelimiter))
                    .Aggregate("", (acc, x) => acc + x);
        }

        string ExportSubscalesAsCSV(string fieldDelimiter, string lineDelimiter)
        {
            return
                GetSubscales()
                    .Select(x => string.Format("SUBSCALE{1}{0}{1}{2}{3}",
                                               x.Name,
                                               fieldDelimiter,
                                               x.Average.ToString(CultureInfo.InvariantCulture),
                                               lineDelimiter))
                    .Aggregate("", (acc, x) => acc + x);
        }
    }
}