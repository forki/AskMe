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
            if (!_questionairePresenter.HasNextItem())
                return;
            var item = _questionairePresenter.AskNextItem();
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

        static Questionaire LoadQuestionaire(string fileName)
        {
            return new QuestionaireParser().Parse(File.ReadAllText(fileName, Encoding.Default));
        }

        void NextButtonClick(object sender, RoutedEventArgs e)
        {
            ShowNextQuestion();
        }
    }
}