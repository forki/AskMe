using System.Windows;

using AskMeItems.Model;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for InstructionWindow.xaml
    /// </summary>
    public partial class InstructionWindow
    {
        public QuestionnairePresenter QuestionnairePresenter { get; private set; }

        public InstructionWindow(QuestionnairePresenter questionnairePresenter)
        {
            QuestionnairePresenter = questionnairePresenter;
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            itemTextBlock.Text = QuestionnairePresenter.Questionnaire.Instruction;
        }
    }
}