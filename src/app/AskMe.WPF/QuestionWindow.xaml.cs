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
        private readonly Questionaire _questionaire;
        private int currentQuestion;

        public QuestionWindow()
        {
            InitializeComponent();

            _questionaire = LoadQuestions(@"C:\data\AskMe\samples\Coded1.txt");

            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            if (currentQuestion >= _questionaire.Questions.Count)
                return;
            Question question = _questionaire.Questions[currentQuestion++];
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
            foreach (Answer answer in question.Answers.Values)
                answersListBox.Items.Add(answer);
        }

        private static Questionaire LoadQuestions(string fileName)
        {
            return new QuestionaireParser().Parse(File.ReadAllText(fileName, Encoding.Default));
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNextQuestion();
        }
    }
}