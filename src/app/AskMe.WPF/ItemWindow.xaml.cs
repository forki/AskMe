using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using AskMe.Model;
using AskMe.TextParser;

namespace AskMe.WPF
{
    /// <summary>
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow
    {
        private readonly Questionaire _questionaire;
        private int currentQuestion;

        public ItemWindow()
        {
            InitializeComponent();

            _questionaire = LoadQuestions(@"C:\data\AskMe\samples\Coded1.txt");

            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            if (currentQuestion >= _questionaire.Items.Count)
                return;
            Item item = _questionaire.Items[currentQuestion++];
            DisplayQuestion(item);
            DisplayAnswers(item);
        }

        private void DisplayQuestion(Item item)
        {
            itemTextBlock.Text = item.Text;
        }

        private void DisplayAnswers(Item item)
        {
            answersListBox.Items.Clear();
            foreach (Answer answer in item.Answers.Values)
                answersListBox.Items.Add(answer);
        }

        private static Questionaire LoadQuestions(string fileName)
        {
            return new QuestionaireParser().Parse(File.ReadAllText(fileName, Encoding.Default));
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            ShowNextQuestion();
        }
    }
}