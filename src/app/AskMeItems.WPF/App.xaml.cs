using System.IO;
using System.Windows;

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

            var window = new BaseWindow(Settings.Default.QuestionnaireFolder, Settings.Default.ResultsPath);
            window.ShowDialog();

            Shutdown();
        }
    }
}