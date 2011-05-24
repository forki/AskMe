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

            new ItemWindow(questionnairePresenter).ShowDialog();
            File.WriteAllText(@".\results\r1.txt", questionnairePresenter.Export());
            Shutdown();
        }
    }
}