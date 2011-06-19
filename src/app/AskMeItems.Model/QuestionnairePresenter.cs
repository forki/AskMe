using System.Collections.Generic;
using System.IO;

using AskMeItems.Model.Export;

namespace AskMeItems.Model
{
    public class QuestionnairePresenter
    {
        public QuestionnairePresenter(string subjectCode, Questionnaire questionnaire)
        {
            SubjectCode = subjectCode;
            Questionnaire = questionnaire;
            Results = new List<Result>();
        }

        public Questionnaire Questionnaire { get; private set; }

        public Item CurrentItem
        {
            get { return Questionnaire.Items[Results.Count]; }
        }

        public List<Result> Results { get; private set; }

        public bool HasIntroduction
        {
            get { return !string.IsNullOrEmpty(Questionnaire.Instruction); }
        }

        protected string SubjectCode { get; private set; }

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

        public string Export(IExporter exporter)
        {
            return exporter.Export(Results, GetSubscales());
        }

        public string GenerateFileName(IExporter exporter)
        {
            return string.Format("{0}_{1}_{2}.txt", exporter.Prefix, Questionnaire.Code, SubjectCode);
        }

        public void ExportToFile(IExporter exporter, string resultsPath)
        {
            var path = new DirectoryInfo(resultsPath);
            if (!path.Exists)
                path.Create();
            var text = Export(exporter);
            File.WriteAllText(Path.Combine(path.FullName, GenerateFileName(exporter)), text);
        }
    }
}