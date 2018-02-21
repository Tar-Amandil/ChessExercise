using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class FenWindow : Window {
        public FenWindow() {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "FEN files (*.fen)|*.fen";

            if (saveFileDialog.ShowDialog() == true) {
                var fen = FenTextBox.Text;
                File.WriteAllText(saveFileDialog.FileName, fen);
            }
        }
    }
}
