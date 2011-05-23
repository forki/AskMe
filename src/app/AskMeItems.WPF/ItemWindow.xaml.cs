using System;
using System.IO;
using System.Text;
using System.Windows;

using AskMeItems.Model;
using AskMeItems.TextParser;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow
    {
        QuestionnairePresenter _questionnairePresenter;

        public ItemWindow()
        {
            InitializeComponent();
        }

        void ShowNextQuestion()
        {
            ReportErrorsInLabel(() =>
            {
                if (!_questionnairePresenter.HasItem())
                    return;
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
            ReportErrorsInLabel(() =>
            {
                _questionnairePresenter =
                    new QuestionnaireParser()
                        .Parse(File.ReadAllText(@"D:\AskMe\samples\Coded1.txt", Encoding.Default))
                        .ToPresenter();
            });

            ShowNextQuestion();
        }
    }
}