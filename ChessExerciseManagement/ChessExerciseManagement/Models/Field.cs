using System;

using ChessExerciseManagement.Pieces;

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
                if (m_piece.Affiliation == value.Affiliation) {
                    throw new ArgumentException("Those pieces cannot capture one another.");
                }

                m_piece?.Capture(value);
                m_piece = value;
                m_piece.SetField(this);
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

        public Field(int x, int y) {
            m_id = ID++;
            X = x;
            Y = y;
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
}
