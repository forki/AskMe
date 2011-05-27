using System;
using System.IO;
using System.Text;
using System.Windows;

using AskMeItems.Model;
using AskMeItems.Model.Export;
using AskMeItems.Model.Parser;
using AskMeItems.WPF.Properties;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var fileName = Path.Combine(Settings.Default.QuestionnaireFolder, Settings.Default.Questionnaire);
            var fileInfo = new FileInfo(fileName);
            var questionnairePresenter =
                new QuestionnairePresenter(new CSVExporter(),
                                           new QuestionnaireParser()
                                               .Parse(File.ReadAllText(fileInfo.FullName, Encoding.Default)));
            Present(questionnairePresenter);

            WriteResults(questionnairePresenter);
            Shutdown();
        }

        static void WriteResults(QuestionnairePresenter questionnairePresenter)
        {
            var path = new DirectoryInfo(Settings.Default.ResultsPath);
            if (!path.Exists)
                path.Create();
            File.WriteAllText(Path.Combine(path.FullName, @"r1.txt"),
                              questionnairePresenter.Export());
        }

        static void Present(QuestionnairePresenter questionnairePresenter)
        {
            switch (questionnairePresenter.Questionnaire.Type)
            {
                case QuestionnaireType.ListedAnswers:
                    new ListedAnswerItemWindow(questionnairePresenter).ShowDialog();
                    break;
                case QuestionnaireType.Likert:
                    new LikertItemWindow(questionnairePresenter).ShowDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}