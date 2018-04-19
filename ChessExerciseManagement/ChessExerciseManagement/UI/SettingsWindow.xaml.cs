using ChessExerciseManagement.Exercises;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();

            FenFolderTextBox.Text = Index.FenFolderPath;
            FolderTextBox.Text = Index.FolderPath;
            FileTextBox.Text = Index.FilePath;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e) {
            FenFolderTextBox.IsEnabled = true;
            FolderTextBox.IsEnabled = true;
            FileTextBox.IsEnabled = true;

            SaveButton.IsEnabled = true;
            ChangeButton.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            if (!validateInput()) {
                return;
            }

            FenFolderTextBox.IsEnabled = false;
            FolderTextBox.IsEnabled = false;
            FileTextBox.IsEnabled = false;

            SaveButton.IsEnabled = false;
            ChangeButton.IsEnabled = true;
        }

        private bool validateInput() {
            return true;
        }
    }
}
