using ChessExerciseManagement.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessExerciseManagement.UI {
    public partial class FieldControl : UserControl {
        private Field m_field;

        private static List<FieldControl> markedFieldControls = new List<FieldControl>();
        private static FieldControl markedFieldControl;

        public FieldControl() {
            InitializeComponent();
        }

        public void SetField(Field field) {
            m_field = field;
            field.FieldControl = this;
            m_field.MyEvent += M_field_MyEvent;
        }

        private void M_field_MyEvent(object sender, PieceEvent e) {
            if (e.Piece == null) {
                imageViewer.Source = null;
            } else {
                imageViewer.Source = e.Piece.GetImage();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (markedFieldControls.Contains(this)) {

                var markedPiece = markedFieldControl.m_field.Piece;
                markedPiece.SetField(m_field);

                MainWindow.WhosTurn = MainWindow.WhosTurn == PlayerAffiliation.Black ? PlayerAffiliation.White : PlayerAffiliation.Black;

                foreach (var fieldControl in markedFieldControls) {
                    fieldControl.BorderBrush = Brushes.Black;
                    fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                    markedFieldControl = null;
                }

                markedFieldControls.Clear();
                markedFieldControl = null;

                return;
                // MessageBox.Show("Whoop");
            }

            foreach (var fieldControl in markedFieldControls) {
                fieldControl.BorderBrush = Brushes.Black;
                fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                markedFieldControl = null;
            }

            markedFieldControls.Clear();

            var piece = m_field.Piece;
            if (piece == null || piece.Affiliation != MainWindow.WhosTurn) {
                return;
            }

            var fields = piece.GetAccessibleFields();

            foreach (var field in fields) {
                var control = field.FieldControl;
                control.BorderBrush = Brushes.Red;
                control.BorderThickness = new Thickness(3.0d);
                markedFieldControls.Add(control);
            }

            markedFieldControl = this;
        }
    }
}
