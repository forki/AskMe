using System;
using System.Windows;

using AskMeItems.Model;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for InstructionPage.xaml
    /// </summary>
    public partial class InstructionPage : INavigationPage
    {
        readonly Action<Action> _safeAction;

        public InstructionPage(Action<Action> safeAction, QuestionnairePresenter questionnairePresenter)
        {
            _safeAction = safeAction;
            QuestionnairePresenter = questionnairePresenter;
            InitializeComponent();
        }

        public QuestionnairePresenter QuestionnairePresenter { get; private set; }

        public bool Next()
        {
            return false;
        }

        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            _safeAction(() => itemTextBlock.Text = QuestionnairePresenter.Questionnaire.Instruction);
        }
    }
}