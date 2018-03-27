using ChessExerciseManagement.Exercises;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class FenWindow : Window {
        public FenWindow() {
            InitializeComponent();
        }

        private void CheckFenButton_Click(object sender, RoutedEventArgs e) {
            var input = FenTextBox.Text ?? string.Empty;
            var lines = input.Split('\n');

            var listOfIllegalFens = new List<int>();

            for (var i = 0; i < lines.Length; i++) {
                var standardFenFlag = CheckStandardFen(lines[i]);
                var jonasFenFlag = CheckJonasFen(lines[i]);

                if (!(standardFenFlag || jonasFenFlag)) {
                    listOfIllegalFens.Add(i);
                }
            }

            foreach (var failedNumber in listOfIllegalFens) {
                MessageBox.Show("FEN in line " + (failedNumber + 1) + " could not be parsed.");
            }
        }

        private bool CheckStandardFen(string fen) {
            if (fen == null || fen.Length == 0) {
                return false;
            }

            return true;
        }

        private bool CheckJonasFen(string fen) {
            if (fen == null || fen.Length == 0) {
                return false;
            }

            var lines = fen.Split(new[] { '\\', '_', '-' });
            var positionCode = lines[0];

            var listOfLegalChars = new List<char>() {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'r', 'R', 'n', 'N', 'b', 'B', 'q', 'Q', 'k', 'K', 'p', 'P'
            };

            foreach (var character in positionCode) {
                if (!listOfLegalChars.Contains(character)) {
                    MessageBox.Show("The character " + character + " is not allowed in this notation.");
                    return false;
                }
            }

            return true;
        }

        private List<int> ExtractNumbersOfJonasFen(string positionCode) {
            var listOfNumbers = new List<int>();
            if (positionCode == null || positionCode.Length == 0) {
                return listOfNumbers;
            }

            var sb = new StringBuilder();
            byte dummyVal;
            foreach (var character in positionCode) {
                var flag = byte.TryParse(character.ToString(), out dummyVal);
                if(flag) {
                    sb.Append()
                }

            }


            return listOfNumbers;
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
