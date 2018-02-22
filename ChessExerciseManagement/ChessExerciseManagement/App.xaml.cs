using ChessExerciseManagement.Exercises;
using System.Windows;

namespace ChessExerciseManagement {
    public partial class App : Application {
        public App() {
            Exit += App_Exit;

            Index.Load();
        }

        private void App_Exit(object sender, ExitEventArgs e) {

        }
    }
}
