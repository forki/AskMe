using System;

namespace AskMeItems.WPF
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : INavigationPage
    {
        readonly Action<string> _setPresenter;


        public SettingsPage(Action<string> setPresenter)
        {
            _setPresenter = setPresenter; 
            InitializeComponent();
        }

        public bool Next()
        {
            _setPresenter(SubjectTextBox.Text);
            return false;
        }
    }
}
