using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using AskMeItems.Model;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for LikertItemWindow.xaml
    /// </summary>
    public partial class LikertItemWindow
    {
        readonly List<Button> _buttons = new List<Button>();
        readonly QuestionnairePresenter _questionnairePresenter;

        public LikertItemWindow(QuestionnairePresenter questionnairePresenter)
        {
            InitializeComponent();
            _questionnairePresenter = questionnairePresenter;
        }

        void FirstSample()
        {
            _buttons.Clear();

            var items = 0;
            foreach (var item in _questionnairePresenter.CurrentItem.Answers)
                _buttons.Add(CreateButton(item,items++));

            
            _buttons.ForEach(x => likertGrid.Children.Add(x));
        }

        static Button CreateButton(KeyValuePair<string, Answer> item, int i)
        {
            return new Button
                   {
                       Content = item.Key,
                       Width = 50,
                       Height = 50,
                       Margin = new Thickness(100*i,100,0,0)
                   };
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
            //answersListBox.Items.Clear();
            //foreach (var answer in _questionnairePresenter.CurrentItem.Answers.Values)
            //    answersListBox.Items.Add(answer);
        }

        void NextButtonClick(object sender, RoutedEventArgs e)
        {
            ReportErrorsInLabel(() =>
            {
                // var answer = answersListBox.SelectedItem as Answer;
                //_questionnairePresenter.AnswerCurrentItem(answer);
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
            FirstSample();
        }
    }
}