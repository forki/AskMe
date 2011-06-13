using System.IO;
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
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var fileName = Path.Combine(Settings.Default.QuestionnaireFolder, Settings.Default.Questionnaire);
            var fileInfo = new FileInfo(fileName);
            var questionnairePresenter =
                new QuestionnairePresenter(new CSVExporter(),
                                           Settings.Default.SubjectCode,
                                           new QuestionnaireParser()
                                               .ParseFromFile(fileInfo.FullName));

            var window = new BaseWindow(questionnairePresenter);
            window.ShowDialog();

            questionnairePresenter.ExportToFile(Settings.Default.ResultsPath);
            Shutdown();
        }
    }
}