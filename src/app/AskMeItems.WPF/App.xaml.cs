using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

using AskMeItems.Model;
using AskMeItems.Model.Export;
using AskMeItems.Model.Parser;

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

            var fileInfo = new FileInfo(e.Args.First());
            var questionnairePresenter =
                new QuestionnairePresenter(new CSVExporter(),
                                           new QuestionnaireParser()
                                               .Parse(File.ReadAllText(fileInfo.FullName, Encoding.Default)));
            Present(questionnairePresenter);

            File.WriteAllText(Path.Combine(fileInfo.Directory.Parent.FullName, @"results\r1.txt"),
                              questionnairePresenter.Export());
            Shutdown();
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