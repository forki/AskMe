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
        readonly List<INavigationPage> _pages;
        readonly QuestionnairePresenter _questionnairePresenter;
        readonly double fontSize;
        int _currentPage;

        public BaseWindow(QuestionnairePresenter questionnairePresenter)
        {
            _questionnairePresenter = questionnairePresenter;
            fontSize = 15;
            InitializeComponent();
            _pages = new List<INavigationPage>();
            if(_questionnairePresenter.HasIntroduction)
                _pages.Add(new InstructionPage(ReportErrorsInLabel, questionnairePresenter));
            _pages.Add(new AnswerItemPage(ReportErrorsInLabel, _questionnairePresenter));

            frame1.NavigationUIVisibility = NavigationUIVisibility.Hidden;
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
                Close();
            else
                frame1.Navigate(_pages[_currentPage]);
        }

        void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            nextButton.FontSize = fontSize;
            nextButton.Margin = new Thickness(e.NewSize.Width - 100, e.NewSize.Height - 80, 0, 0);
        }

        void GridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            frame1.Width = e.NewSize.Width - 30;
            frame1.Height = e.NewSize.Height - 30;
        }
    }
}