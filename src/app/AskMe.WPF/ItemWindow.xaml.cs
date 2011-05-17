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
        readonly QuestionnairePresenter _questionnairePresenter;

        public ItemWindow()
        {
            InitializeComponent();

            _questionnairePresenter = new QuestionnairePresenter(LoadQuestionaire(@"D:\AskMe\samples\Coded1.txt"));

            ShowNextQuestion();
        }

        void ShowNextQuestion()
        {
            if (!_questionnairePresenter.HasItem())
                return;
            DisplayQuestion();
            DisplayAnswers();
        }

        void DisplayQuestion()
        {
            itemTextBlock.Text = _questionnairePresenter.CurrentItem.Text;
        }

        void DisplayAnswers()
        {
            answersListBox.Items.Clear();
            foreach (var answer in _questionnairePresenter.CurrentItem.Answers.Values)
                answersListBox.Items.Add(answer);
        }

        static Questionnaire LoadQuestionaire(string fileName)
        {
            return new QuestionnaireParser().Parse(File.ReadAllText(fileName, Encoding.Default));
        }

        void NextButtonClick(object sender, RoutedEventArgs e)
        {
            var answer = answersListBox.SelectedItem as Answer;
            _questionnairePresenter.AnswerCurrentItem(answer);
            ShowNextQuestion();
        }
    }
}