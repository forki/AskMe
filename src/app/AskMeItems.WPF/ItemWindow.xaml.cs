using System;
using System.Windows;

using AskMeItems.Model;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow
    {
        readonly QuestionnairePresenter _questionnairePresenter;

        public ItemWindow(QuestionnairePresenter questionnairePresenter)
        {
            InitializeComponent();
            _questionnairePresenter = questionnairePresenter;
        }

        void ShowNextQuestion()
        {
            ReportErrorsInLabel(() =>
            {
                if (!_questionnairePresenter.HasItem())
                {
                    Close();
                    return;
                }
                DisplayQuestion();
                DisplayAnswers();
            });
        }

        void DisplayQuestion()
        {
            itemTextBlock.Text = _questionnairePresenter.CurrentItem.Text;
        }

        void DisplayAnswers()
        {
            answersListBox.Items.Clear();
            foreach (var answer in _questionnairePresenter.CurrentItem.Answers.Values)
                answersListBox.Items.Add(answer);
        }

        void NextButtonClick(object sender, RoutedEventArgs e)
        {
            ReportErrorsInLabel(() =>
            {
                var answer = answersListBox.SelectedItem as Answer;
                _questionnairePresenter.AnswerCurrentItem(answer);
                ShowNextQuestion();
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

        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ShowNextQuestion();
        }
    }
}