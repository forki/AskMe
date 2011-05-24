using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

using AskMeItems.Model;
using AskMeItems.TextParser;

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

            var fileName = e.Args.First();
            var questionnairePresenter =
                new QuestionnaireParser()
                    .Parse(File.ReadAllText(fileName, Encoding.Default))
                    .ToPresenter();

            new ItemWindow(questionnairePresenter).ShowDialog();
            Shutdown();
        }
    }
}