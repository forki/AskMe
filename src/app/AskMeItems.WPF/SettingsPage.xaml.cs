using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace AskMeItems.WPF
{
    /// <summary>
    ///   Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : INavigationPage
    {
        readonly DirectoryInfo _directoryInfo;
        readonly Action<Action> _safeAction;
        readonly Action<List<string>, string> _setPresenter;
        int _clicked;
 
        public SettingsPage(Action<Action> safeAction, string directory, Action<List<string>, string> setPresenter)
        {
            _safeAction = safeAction;
            _setPresenter = setPresenter;
            InitializeComponent();
            QuestionnairesBox.Items.Clear();
            _directoryInfo = new DirectoryInfo(directory);

            _safeAction(() =>
            {
                var files =
                    _directoryInfo.GetFiles("*.txt", SearchOption.AllDirectories)
                        .Select(fi => new ListBoxItem {Content = CreatePanel(fi.Name)});

                foreach (var file in files)
                {
                    file.Selected += (sender, e) =>
                    {
                        var x = (ListBoxItem) sender;
                        var c = (StackPanel)x.Content;
                        var textBox = (TextBox)c.Children[0];
                        textBox.Text = (++_clicked).ToString();
                    };
                    QuestionnairesBox.Items.Add(file);
                }
            });
        }

        IEnumerable<Tuple<string, int>> GetQuestionnaireOrder
        {
            get
            {
                return
                    QuestionnairesBox.Items.Cast<ListBoxItem>()
                        .Select(item =>
                        {
                            var c = (StackPanel) item.Content;
                            var label = (Label) c.Children[1];
                            var textBox = (TextBox) c.Children[0];
                            
                            var text = textBox.Text;
                            if (string.IsNullOrEmpty(text))
                                text = "0";
                            var pos = int.Parse(text);
                            return Tuple.Create(label.Content as string, pos);
                        });
            }
        }

        public bool Next()
        {
            var ok = true;
            _safeAction(() =>
            {
                if (string.IsNullOrEmpty(SubjectTextBox.Text))
                {
                    ok = false;
                    throw new Exception(Properties.Resources.FillInSubjectCode);
                }
            });

            if (!ok)
                return true;

            var selected = new List<string>();

            ok = false;

            _safeAction(() =>
            {
                selected =
                    GetQuestionnaireOrder
                        .Where(x => x.Item2 != 0)
                        .OrderBy(x => x.Item2)
                        .Select(x => x.Item1)
                        .Select(file => Path.Combine(_directoryInfo.FullName, file))
                        .ToList();

                if(!selected.Any())
                    throw new Exception(Properties.Resources.NoQuestionnaireSelected);
                ok = true;
            });
            if (!ok)
                return true;

            _setPresenter(selected, SubjectTextBox.Text);
            return false;
        }

        public Panel CreatePanel(string text)
        {
            var panel = new StackPanel {Orientation = Orientation.Horizontal};
            panel.Children.Add(new TextBox {Text = "", Width = 30});
            panel.Children.Add(new Label {Content = text});
            return panel;
        }
    }
}