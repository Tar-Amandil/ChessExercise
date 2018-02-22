using System;
using System.Collections.Generic;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class KeywordWindow : Window {
        public List<string> Keywords {
            private set;
            get;
        } = new List<string>();

        public KeywordWindow() {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            var text = KeywordTextBox.Text;

            var keywords = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var keyword in keywords) {
                Keywords.Add(keyword.Replace(" ", string.Empty));
            }

            Close();
        }
    }
}
