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
        readonly QuestionairePresenter _questionairePresenter;

        public ItemWindow()
        {
            InitializeComponent();

            _questionairePresenter = new QuestionairePresenter(LoadQuestionaire(@"D:\AskMe\samples\Coded1.txt"));

            ShowNextQuestion();
        }

        void ShowNextQuestion()
        {
            if (!_questionairePresenter.HasItem())
                return;
            DisplayQuestion();
            DisplayAnswers();
        }

        void DisplayQuestion()
        {
            itemTextBlock.Text = _questionairePresenter.CurrentItem.Text;
        }

        void DisplayAnswers()
        {
            answersListBox.Items.Clear();
            foreach (var answer in _questionairePresenter.CurrentItem.Answers.Values)
                answersListBox.Items.Add(answer);
        }

        static Questionaire LoadQuestionaire(string fileName)
        {
            return new QuestionaireParser().Parse(File.ReadAllText(fileName, Encoding.Default));
        }

        void NextButtonClick(object sender, RoutedEventArgs e)
        {
            var answer = answersListBox.SelectedItem as Answer;
            _questionairePresenter.AnswerCurrentItem(answer);
            ShowNextQuestion();
        }
    }
}