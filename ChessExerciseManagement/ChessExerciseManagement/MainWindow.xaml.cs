using System.Windows;

using ChessExerciseManagement.UI;

namespace ChessExerciseManagement {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void TrainingsButton_Click(object sender, RoutedEventArgs e) {
            var trainingWindow = new TrainingWindow();
            trainingWindow.ShowDialog();
        }
    }
}
