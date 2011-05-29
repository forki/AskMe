using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using AskMeItems.Model;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for AnswerItemPage.xaml
    /// </summary>
    public partial class AnswerItemPage : INavigationPage
    {
        readonly QuestionnairePresenter _questionnairePresenter;
        readonly Action<Action> _safeAction;

        public AnswerItemPage(Action<Action> safeAction, QuestionnairePresenter questionnairePresenter)
        {
            InitializeComponent();
            _safeAction = safeAction;
            _questionnairePresenter = questionnairePresenter;
            SetListBoxStyle(questionnairePresenter);
        }

        public bool Next()
        {
            _safeAction(() =>
            {
                var answer = ((ListBoxItem) answersListBox.SelectedItem).Content as Answer;
                _questionnairePresenter.AnswerCurrentItem(answer);
                DisplayQuestion(Width, Height);
            });
            return _questionnairePresenter.HasItem();
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
            if (!_questionnairePresenter.HasItem())
                return;

            var tuple = CalculateFontSizeAndTextWidth(width, 10);
            var maxWidth = tuple.Item2;
            var fontSize = tuple.Item1;

            itemTextBlock.Text = _questionnairePresenter.CurrentItem.Text;

            itemLabel.Width = width / 1.2;
            itemLabel.Height = height / 4;
            itemTextBlock.FontSize = width / 30;

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

        void GridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            DisplayQuestion(e.NewSize.Width, e.NewSize.Height);
        }
    }
}