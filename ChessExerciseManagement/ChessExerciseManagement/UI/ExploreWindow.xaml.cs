using ChessExerciseManagement.Exercises;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class ExploreWindow : Window {
        public ExploreWindow() {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            var text = KeywordTexbox.Text;
            var keywords = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var list = new List<string>();

            for (var i = 0; i < keywords.Length; i++) {
                list.Add(keywords[i].Replace(" ", string.Empty));
            }

            var exercises = ExerciseManager.Filter(list);
            ExerciseListBox.ItemsSource = exercises;
        }

        private void ExerciseListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (ExerciseListBox.SelectedItem == null) {
                return;
            }

            var viewWindow = new ViewWindow(ExerciseListBox.SelectedItem as string);
            viewWindow.ShowDialog();
        }
    }
}
