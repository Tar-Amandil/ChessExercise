using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;

using ChessExerciseManagement.Models;
using ChessExerciseManagement.Events;

namespace ChessExerciseManagement.UI.UserControls {
    public partial class FieldControl : UserControl {
        private Field m_field;
        private BoardControl m_boardControl;

        private bool m_readonly;

        public FieldControl() {
            InitializeComponent();
        }

        public void SetField(Field field) {
            m_field = field;
            m_field.PieceChange += Field_PieceChange;

            if (field.X % 2 == field.Y % 2) {
                Background = Brushes.AliceBlue;
            } else {
                Background = Brushes.RosyBrown;
            }

            imageViewer.Source = field.Piece?.GetImage();
        }

        public void SetBoardControl(BoardControl boardControl) {
            m_boardControl = boardControl;
        }

        public void SetReadonly(bool read) {
            m_readonly = read;
        }

        private void Field_PieceChange(object sender, PieceEvent e) {
            if (e.Piece == null) {
                imageViewer.Source = null;
            } else {
                imageViewer.Source = e.Piece.GetImage();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (m_readonly) {
                return;
            }

            var game = TrainingWindow.Game;
            var markedFieldControls = m_boardControl.MarkedFieldControls;

            if (markedFieldControls.Contains(this)) {
                var markedPiece = m_boardControl.MarkedFieldControl.m_field.Piece;
                markedPiece.SetField(m_field);


                foreach (var fieldControl in markedFieldControls) {
                    fieldControl.BorderBrush = Brushes.Black;
                    fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                    m_boardControl.MarkedFieldControl = null;
                }

                markedFieldControls.Clear();
                m_boardControl.MarkedFieldControl = null;

                return;
            }

            foreach (var fieldControl in markedFieldControls) {
                fieldControl.BorderBrush = Brushes.Black;
                fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                m_boardControl.MarkedFieldControl = null;
            }

            markedFieldControls.Clear();

            var piece = m_field.Piece;
            if (piece == null || piece.Affiliation != game.WhosTurn) {
                return;
            }

            var fields = piece.GetAccessibleFields();

            foreach (var field in fields) {
                var x = field.X;
                var y = field.Y;

                var control = m_boardControl.Controls[x, y];

                control.BorderBrush = Brushes.Red;
                control.BorderThickness = new Thickness(3.0d);
                markedFieldControls.Add(control);
            }

            m_boardControl.MarkedFieldControl = this;
        }
    }
}
