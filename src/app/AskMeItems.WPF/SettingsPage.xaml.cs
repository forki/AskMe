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
        readonly List<CheckBox> _checkBoxes = new List<CheckBox>();
        readonly DirectoryInfo _directoryInfo;
        readonly Action<Action> _safeAction;
        readonly Action<List<string>, string> _setPresenter;

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
                        .Select(fi =>
                        {
                            var cb = new CheckBox {Content = fi.Name};
                            _checkBoxes.Add(cb);
                            return cb;
                        })
                        .Select(cb => new ListBoxItem {Content = cb});
                foreach (var file in files)
                    QuestionnairesBox.Items.Add(file);
            });
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

            var selected =
                _checkBoxes
                    .Where(cb => cb.IsChecked == true)
                    .Select(checkBox => (string) checkBox.Content)
                    .Select(file => Path.Combine(_directoryInfo.FullName, file))
                    .ToList();

            _setPresenter(selected, SubjectTextBox.Text);
            return false;
        }
    }
}