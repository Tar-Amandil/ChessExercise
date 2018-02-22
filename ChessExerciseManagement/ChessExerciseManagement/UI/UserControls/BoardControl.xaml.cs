using System.Windows.Controls;

using ChessExerciseManagement.Models;
using System.Collections.Generic;

namespace ChessExerciseManagement.UI.UserControls {
    public partial class BoardControl : UserControl {
        private Board m_board;
        public Board Board {
            set {
                m_board = value;

                var fields = value.Fields;
                for (int y = 0; y < 8; y++) {
                    for (int x = 0; x < 8; x++) {
                        Controls[x, y].SetField(fields[x, y]);
                    }
                }
            }
            get {
                return m_board;
            }
        }

        public readonly FieldControl[,] Controls;

        public readonly List<FieldControl> MarkedFieldControls;
        public FieldControl MarkedFieldControl;

        private bool m_readonly;

        public BoardControl() {
            InitializeComponent();

            Controls = new FieldControl[8, 8];
            MarkedFieldControls = new List<FieldControl>();

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    var identifier = "f" + x + y;
                    Controls[x, y] = (FieldControl)FindName(identifier);
                    Controls[x, y].SetBoardControl(this);
                }
            }
        }

        public void SetReadonly(bool read) {
            m_readonly = read;
            foreach(var contr in Controls) {
                contr.SetReadonly(read);
            }
        }
    }
}
