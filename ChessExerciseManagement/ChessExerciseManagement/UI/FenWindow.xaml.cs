using ChessExerciseManagement.Exercises;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class FenWindow : Window {
        public FenWindow() {
            InitializeComponent();
        }

        private void CheckFenButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Not yet implemented");
        }

        private void SaveFenButton_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "FEN files (*.fen)|*.fen";

            if (saveFileDialog.ShowDialog() == true) {
                var fen = FenTextBox.Text;
                File.WriteAllText(saveFileDialog.FileName, fen);
            }
        }

        private void SaveExerciseButton_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "FEN files (*.fen)|*.fen";

            if (saveFileDialog.ShowDialog() == true) {
                var fen = FenTextBox.Text;
                var filename = saveFileDialog.FileName;
                File.WriteAllText(filename, fen);

                var keywordWindow = new KeywordWindow();
                keywordWindow.ShowDialog();

                var keywords = keywordWindow.Keywords;

                Index.AddFile(filename, keywords);
                ExerciseManager.AddExercise(filename, keywords);
            }
        }
    }
}
