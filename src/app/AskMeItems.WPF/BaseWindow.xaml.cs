using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;

using AskMeItems.Model;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        readonly QuestionnairePresenter _questionnairePresenter;
        readonly double fontSize;
        readonly List<INavigationPage> pages;
        int _currentPage;

        public BaseWindow(QuestionnairePresenter questionnairePresenter)
        {
            _questionnairePresenter = questionnairePresenter;
            fontSize = 15;
            InitializeComponent();
            pages =
                new List<INavigationPage>
                {
                    new InstructionPage(ReportErrorsInLabel, questionnairePresenter),
                    new AnswerItemPage(ReportErrorsInLabel, _questionnairePresenter)
                };

            frame1.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frame1.Navigate(pages[_currentPage]);
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
            if (pages[_currentPage].Next())
                return;
            _currentPage++;
            if (pages.Count <= _currentPage)
                Close();
            else
                frame1.Navigate(pages[_currentPage]);
        }

        void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            nextButton.FontSize = fontSize;
            nextButton.Margin = new Thickness(e.NewSize.Width - 100, e.NewSize.Height - 80, 0, 0);
        }
    }
}