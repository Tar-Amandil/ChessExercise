using System;

using ChessExerciseManagement.Pieces;
using ChessExerciseManagement.UI;

namespace ChessExerciseManagement.Models {
    public class Field {
        public int X {
            private set;
            get;
        }

        public int Y {
            private set;
            get;
        }

        private Piece m_piece;
        public Piece Piece {
            set {
                if (m_piece?.Affiliation == value?.Affiliation) {
                    throw new ArgumentException("Those pieces cannot capture one another.");
                }

                m_piece?.Capture(value);
                m_piece = value;
                //m_piece.SetField(this);

                OnPieceChange(value);
            }
            get {
                return m_piece;
            }
        }

        private static int ID;
        private int m_id {
            set;
            get;
        }

        public event CustomEventHandler MyEvent;
        public delegate void CustomEventHandler(object sender, PieceEvent e);

        public FieldControl FieldControl {
            set;
            get;
        }

        public Field(int x, int y) {
            m_id = ID++;
            X = x;
            Y = y;
        }

        public void OnPieceChange(Piece piece) {
            MyEvent?.Invoke(this, new PieceEvent(piece));
        }

        public bool Equals(Field other) {
            return other.m_id == m_id;
        }

        public override bool Equals(object obj) {
            if (!(obj is Field)) {
                return false;
            }

            return Equals(obj as Field);
        }

        public override int GetHashCode() {
            return m_id;
        }
    }

    public class PieceEvent {
        public Piece Piece {
            private set;
            get;
        }

        public PieceEvent(Piece piece) {
            Piece = piece;
        }
    }
}
