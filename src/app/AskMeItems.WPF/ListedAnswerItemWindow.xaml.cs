using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using AskMeItems.Model;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for ListedAnswerItemWindow.xaml
    /// </summary>
    public partial class ListedAnswerItemWindow
    {
        readonly QuestionnairePresenter _questionnairePresenter;

        public ListedAnswerItemWindow(QuestionnairePresenter questionnairePresenter)
        {
            InitializeComponent();
            _questionnairePresenter = questionnairePresenter;
            SetListBoxStyle(questionnairePresenter);
        }

        void SetListBoxStyle(QuestionnairePresenter questionnairePresenter)
        {
            switch (questionnairePresenter.Questionnaire.Type)
            {
                case QuestionnaireType.ListedAnswers:
                    answersListBox.Style = null;
                    break;
                case QuestionnaireType.Likert:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
                DisplayQuestion(Width, Height);
            });
        }

        public Tuple<double, double> CalculateFontSizeAndTextWidth(double width, double fontSize)
        {
            var answers = _questionnairePresenter.CurrentItem.Answers.Values.Select(x => x.Text).ToList();
            var maxWidth =
                answers
                    .Select(answer => FontSizeCalculator.GetFontWidth(answer, answersListBox.FontFamily, fontSize))
                    .Max();

            var sum = maxWidth * answers.Count();
            return sum + width * 0.45 < width
                       ? CalculateFontSizeAndTextWidth(width, fontSize + 0.5)
                       : Tuple.Create(fontSize, maxWidth);
        }

        void DisplayQuestion(double width, double height)
        {
            var tuple = CalculateFontSizeAndTextWidth(width, 10);
            var maxWidth = tuple.Item2;
            var fontSize = tuple.Item1;

            itemTextBlock.Text = _questionnairePresenter.CurrentItem.Text;

            itemLabel.Width = width / 1.2;
            itemLabel.Height = height / 4;
            itemTextBlock.FontSize = width / 30;
            nextButton.FontSize = fontSize;
            nextButton.Margin = new Thickness(width - 100, height - 80, 0, 0);

            answersListBox.Items.Clear();
            var answers = _questionnairePresenter.CurrentItem.Answers;
            var items =
                answers.Values
                    .Select(answer => new ListBoxItem
                                      {
                                          Content = answer,
                                          FontSize = fontSize,
                                          Width = maxWidth + 0.4 * maxWidth
                                      });

            foreach (var listBoxItem in items)
                answersListBox.Items.Add(listBoxItem);
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

        void GridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            DisplayQuestion(e.NewSize.Width, e.NewSize.Height);
        }
    }
}