using System;
using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public abstract class Piece {
        public PlayerAffiliation Affiliation {
            private set;
            get;
        }

        public Player Player {
            private set;
            get;
        }

        public Field Field {
            private set;
            get;
        }

        public int MoveCounter {
            private set;
            get;
        }

        private static int ID;
        private int m_id {
            set;
            get;
        }

        public Piece(Player player) {
            if (player == null) {
                throw new ArgumentNullException("Player must not be null.");
            }

            m_id = ID++;
            Player = player;
            Affiliation = player.Affiliation;
        }

        public void Capture(Piece capturingPiece) {
            Player.NotifyCapturedPiece(this, capturingPiece);
            Field = null;
        }

        public void SetField(Field field, bool count = true) {
            Field = field;
            if (count) {
                MoveCounter++;
            }
        }

        public abstract List<Field> GetAccessibleFields(Board board);

        public bool Equals(Piece other) {
            return other.m_id == m_id;
        }

        public override bool Equals(object obj) {
            if (!(obj is Piece)) {
                return false;
            }

            return Equals(obj as Piece);
        }

        public override int GetHashCode() {
            return m_id;
        }
    }
}
