using System.Collections.Generic;
using System.IO;

using AskMeItems.Model.Export;

namespace AskMeItems.Model
{
    public class QuestionnairePresenter
    {
        public QuestionnairePresenter(IExporter exporter, string subjectCode, Questionnaire questionnaire)
        {
            SubjectCode = subjectCode;
            Exporter = exporter;
            Questionnaire = questionnaire;
            Results = new List<Result>();
        }

        public IExporter Exporter { get; set; }
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

        public string Export()
        {
            return Exporter.Export(Results, GetSubscales());
        }

        public void ExportToFile(string dir)
        {
            var path = new DirectoryInfo(dir);
            if (!path.Exists)
                path.Create();
            File.WriteAllText(Path.Combine(path.FullName, GenerateFileName()), Export());
        }

        public string GenerateFileName()
        {
            return string.Format("Result_{0}_{1}.txt", Questionnaire.Code, SubjectCode);
        }
    }
}