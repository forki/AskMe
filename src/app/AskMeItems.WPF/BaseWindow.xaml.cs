using System;
using System.Windows;
using System.Windows.Controls;

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
        Page _currentPage;

        public BaseWindow(QuestionnairePresenter questionnairePresenter)
        {
            _questionnairePresenter = questionnairePresenter;
            fontSize = 15;
            InitializeComponent();
            _currentPage = new InstructionPage(ReportErrorsInLabel, questionnairePresenter);

            frame1.Navigate(_currentPage);
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
            var answerItemPage = _currentPage as AnswerItemPage;
            if (answerItemPage != null)
            {
                if (!answerItemPage.NextQuestion())
                    Close();
            }
            else
            {
                _currentPage = new AnswerItemPage(ReportErrorsInLabel, _questionnairePresenter);
                frame1.Navigate(_currentPage);
            }
        }

        void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            nextButton.FontSize = fontSize;
            nextButton.Margin = new Thickness(e.NewSize.Width - 100, e.NewSize.Height - 80, 0, 0);
        }
    }
}