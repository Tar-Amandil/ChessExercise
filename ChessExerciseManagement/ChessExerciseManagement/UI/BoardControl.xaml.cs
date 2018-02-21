using System.Windows.Controls;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.UI {
    public partial class BoardControl : UserControl {
        private Board m_board;
        public Board Board {
            set {
                m_board = value;

                var fields = value.Fields;
                for (int y = 0; y < 8; y++) {
                    for (int x = 0; x < 8; x++) {
                        controls[x, y].SetField(fields[x, y]);
                    }
                }
            }
            get {
                return m_board;
            }
        }

        public readonly FieldControl[,] controls;

        public BoardControl() {
            InitializeComponent();

            controls = new FieldControl[8, 8];

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    var identifier = "f" + x + y;
                    controls[x, y] = (FieldControl)FindName(identifier);
                }
            }
        }
    }
}
