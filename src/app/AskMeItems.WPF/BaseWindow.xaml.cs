using System;
using System.Collections.Generic;
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
        readonly string _fileName;
        readonly double _fontSize;
        readonly List<INavigationPage> _pages;
        int _currentPage;
        readonly string _resultsPath;

        public BaseWindow(string fileName, string resultsPath)
        {
            _fileName = fileName;
            _resultsPath = resultsPath;
            _fontSize = 15;
            InitializeComponent();


            ErrorLabel.Content = "";
            _pages = new List<INavigationPage> {new SettingsPage(SetPresenter)};

            frame1.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frame1.Navigate(_pages[_currentPage]);            
        }

        public QuestionnairePresenter QuestionnairePresenter { get; private set; }

        void SetPresenter(string subjectCode)
        {
            var questionnaire = new QuestionnaireParser().ParseFromFile(_fileName);

            QuestionnairePresenter =
                new QuestionnairePresenter(new CSVExporter(), subjectCode, questionnaire);

            if (QuestionnairePresenter.HasIntroduction)
                _pages.Add(new InstructionPage(ReportErrorsInLabel, QuestionnairePresenter));
            _pages.Add(new AnswerItemPage(ReportErrorsInLabel, QuestionnairePresenter));

            frame1.Navigate(_pages[_currentPage]);
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
            if (_pages[_currentPage].Next())
                return;
            _currentPage++;
            if (_pages.Count <= _currentPage)
            {
                QuestionnairePresenter.ExportToFile(_resultsPath);
                Close();
            }
            else
                frame1.Navigate(_pages[_currentPage]);
        }

        void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            nextButton.FontSize = _fontSize;
            nextButton.Margin = new Thickness(e.NewSize.Width - 100, e.NewSize.Height - 80, 0, 0);
        }

        void GridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            frame1.Width = e.NewSize.Width - 30;
            frame1.Height = e.NewSize.Height - 30;
        }
    }
}