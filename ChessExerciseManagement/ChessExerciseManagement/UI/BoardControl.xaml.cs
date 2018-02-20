using ChessExerciseManagement.Models;
using System.Windows.Controls;
using System;

namespace ChessExerciseManagement.UI {
    public partial class BoardControl : UserControl {
        Board chessBoard;
        FieldControl[,] controls;

        public BoardControl() {
            InitializeComponent();
            InitControls();
        }

        private void InitControls() {
            controls = new FieldControl[8, 8];

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    var identifier = "f" + x + (7 - y);
                    controls[x, y] = (FieldControl)FindName(identifier);
                }
            }
        }

        public void SetBoard(Board board) {
            chessBoard = board;

            var fields = board.Fields;
            for (int y = 0; y < board.Height; y++) {
                for (int x = 0; x < board.Width; x++) {
                    controls[x, y].SetField(fields[x, y]);
                }
            }
        }
    }
}
