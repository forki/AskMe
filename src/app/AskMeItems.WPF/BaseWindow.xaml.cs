using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

using AskMeItems.Model;
using AskMeItems.Model.Export;
using AskMeItems.Model.Parser;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        readonly List<IExporter> _exporters =
            new List<IExporter>
            {
                new CSVExporterWithSubscales(),
                new CSVExporter()
            };

        readonly double _fontSize;
        readonly List<INavigationPage> _pages;
        readonly string _resultsPath;
        int _currentPage;
        List<QuestionnairePresenter> _questionnairePresenters = new List<QuestionnairePresenter>();

        public BaseWindow(string directory, string resultsPath)
        {
            _resultsPath = resultsPath;
            _fontSize = 15;
            InitializeComponent();

            ErrorLabel.Content = "";
            _pages =
                new List<INavigationPage>
                {
                    new SettingsPage(ReportErrorsInLabel, directory, SetPresenter)
                };

            frame1.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frame1.Navigate(_pages[_currentPage]);
        }

        void SetPresenter(IEnumerable<string> fileNames, string subjectCode)
        {
            ReportErrorsInLabel(() =>
            {
                _questionnairePresenters =
                    fileNames.Select(file => new QuestionnaireParser().ParseFromFile(file))
                        .Select(questionnaire => new QuestionnairePresenter(subjectCode, questionnaire))
                        .ToList();

                foreach (var presenter in _questionnairePresenters)
                {
                    if (presenter.HasIntroduction)
                        _pages.Add(new InstructionPage(ReportErrorsInLabel, presenter));
                    _pages.Add(new AnswerItemPage(ReportErrorsInLabel, presenter));
                }
                frame1.Navigate(_pages[_currentPage]);
            });
        }

        void ReportErrorsInLabel(Action action)
        {
            try
            {
                ErrorLabel.Content = "";
                action();
            }
            catch (Exception ex)
            {
                ErrorLabel.Content = ex.Message;
            }
        }

        void NextButtonClick(object sender, RoutedEventArgs e)
        {
            SaveAllResults();

            if (_pages[_currentPage].Next())
                return;
            _currentPage++;
            if (_pages.Count <= _currentPage)
                Close();
            else
                frame1.Navigate(_pages[_currentPage]);
        }

        void SaveAllResults()
        {
            foreach (var exporter in _exporters)
                foreach (var presenter in _questionnairePresenters)
                    presenter.ExportToFile(exporter, _resultsPath);
        }

        void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            NextButton.FontSize = _fontSize;
            NextButton.Margin = new Thickness(e.NewSize.Width - 100, e.NewSize.Height - 80, 0, 0);
            ErrorLabel.FontSize = _fontSize;
            ErrorLabel.Margin = new Thickness(100, e.NewSize.Height - 80, 0, 0);
        }

        void GridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            frame1.Width = e.NewSize.Width - 30;
            frame1.Height = e.NewSize.Height - 30;
        }
    }
}