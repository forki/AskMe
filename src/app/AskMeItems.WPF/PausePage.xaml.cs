namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for PausePage.xaml
    /// </summary>
    public partial class PausePage : INavigationPage
    {
        public PausePage()
        {
            InitializeComponent();
            pauseTextBlock.Text = Properties.Resources.PauseText;
        }

        public bool Next()
        {
            return false;
        }
    }
}