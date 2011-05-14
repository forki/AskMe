using System.IO;
using System.Text;
using System.Windows;

using AskMe.Model;
using AskMe.TextParser;

namespace AskMe.WPF
{
    /// <summary>
    ///   Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow
    {
        readonly Questionaire _questionaire;
        int currentQuestion;

        public ItemWindow()
        {
            InitializeComponent();

            _questionaire = LoadQuestions(@"C:\data\AskMe\samples\Coded1.txt");

            ShowNextQuestion();
        }

        void ShowNextQuestion()
        {
            if (currentQuestion >= _questionaire.Items.Count)
                return;
            var item = _questionaire.Items[currentQuestion++];
            DisplayQuestion(item);
            DisplayAnswers(item);
        }

        void DisplayQuestion(Item item)
        {
            itemTextBlock.Text = item.Text;
        }

        void DisplayAnswers(Item item)
        {
            answersListBox.Items.Clear();
            foreach (var answer in item.Answers.Values)
                answersListBox.Items.Add(answer);
        }

        static Questionaire LoadQuestions(string fileName)
        {
            return new QuestionaireParser().Parse(File.ReadAllText(fileName, Encoding.Default));
        }

        void NextButtonClick(object sender, RoutedEventArgs e)
        {
            ShowNextQuestion();
        }
    }
}