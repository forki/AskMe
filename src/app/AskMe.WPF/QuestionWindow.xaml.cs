using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using AskMe.Model;
using AskMe.TextParser;

namespace AskMe.WPF
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow
    {
        private readonly List<Question> _questions;
        private int currentQuestion;

        public QuestionWindow()
        {
            InitializeComponent();

            _questions = LoadQuestions(@"C:\data\AskMe\samples\Simple1.txt");

            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            if (currentQuestion >= _questions.Count)
                return;
            Question question = _questions[currentQuestion++];
            DisplayQuestion(question);
            DisplayAnswers(question);
        }

        private void DisplayQuestion(Question question)
        {
            questionLabel.Content = question.Text;
        }

        private void DisplayAnswers(Question question)
        {
            answersListBox.Items.Clear();
            foreach (Answer answer in question.Answers)
                answersListBox.Items.Add(answer);
        }

        private static List<Question> LoadQuestions(string fileName)
        {
            return new QuestionaireParser().Parse(File.ReadAllText(fileName, Encoding.Default));
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNextQuestion();
        }
    }
}