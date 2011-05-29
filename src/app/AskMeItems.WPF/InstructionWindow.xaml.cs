using System;
using System.Windows;

using AskMeItems.Model;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for InstructionWindow.xaml
    /// </summary>
    public partial class InstructionWindow
    {
        public InstructionWindow(QuestionnairePresenter questionnairePresenter)
        {
            QuestionnairePresenter = questionnairePresenter;
            InitializeComponent();
        }

        public QuestionnairePresenter QuestionnairePresenter { get; private set; }

        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ReportErrorsInLabel(() => itemTextBlock.Text = QuestionnairePresenter.Questionnaire.Instruction);
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

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}