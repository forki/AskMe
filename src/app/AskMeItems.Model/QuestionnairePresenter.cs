using System.Collections.Generic;
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
                Results
                    .Select(x => string.Format("{0}{1}{2}{1}{3}{4}",
                                               x.Item.Code,
                                               FieldDelimiter,
                                               x.SelectedAnswer.Code,
                                               x.Points,
                                               LineDelimiter))                                               
                    .Aggregate("", (acc,x) => acc + x);
        }
    }
}