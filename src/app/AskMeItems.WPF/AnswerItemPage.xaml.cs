using System;
using System.Collections.Generic;
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
        readonly Dictionary<ListBoxItem, Answer> _itemsDict = new Dictionary<ListBoxItem, Answer>();
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
                Answer answer = null;
                var listBoxItem = answersListBox.SelectedItem as ListBoxItem;
                if (listBoxItem != null)
                    answer = _itemsDict[listBoxItem];
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
                    .Select(answer => 1.6 * FontSizeCalculator.GetFontWidth(answer, answersListBox.FontFamily, fontSize))
                    .Max();

            var sum = maxWidth * 1.3 * answers.Count();
            return sum < width
                       ? CalculateFontSizeAndTextWidth(width, fontSize + 0.5)
                       : Tuple.Create(fontSize, maxWidth);
        }

        void DisplayQuestion(double width, double height)
        {
            if (!_questionnairePresenter.HasItem())
                return;

            var tuple = CalculateFontSizeAndTextWidth(width, 1);
            var buttonWidth = tuple.Item2;
            var fontSize = tuple.Item1;

            itemTextBlock.Text = _questionnairePresenter.CurrentItem.Text;

            itemLabel.Width = width / 1.2;
            itemLabel.Height = height / 4;
            itemTextBlock.FontSize = width / 30;

            answersListBox.Items.Clear();
            var answers = _questionnairePresenter.CurrentItem.Answers;
            _itemsDict.Clear();

            var items =
                answers.Values
                    .Select(answer =>
                    {
                        var textBlock = new TextBlock
                                        {
                                            Text = answer.ToString(),
                                            FontSize = fontSize,
                                            TextAlignment = TextAlignment.Center
                                        };
                        var item = new ListBoxItem
                                   {
                                       Content = textBlock,
                                       Width = buttonWidth,
                                       HorizontalContentAlignment = HorizontalAlignment.Center
                                   };
                        _itemsDict.Add(item, answer);
                        return item;
                    });

            foreach (var listBoxItem in items)
                answersListBox.Items.Add(listBoxItem);
        }

        void GridSizeChanged
            (object sender, SizeChangedEventArgs e)
        {
            DisplayQuestion(e.NewSize.Width, e.NewSize.Height);
        }
    }
}