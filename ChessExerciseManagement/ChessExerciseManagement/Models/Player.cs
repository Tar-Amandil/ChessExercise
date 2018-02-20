using System.Collections.Generic;

using ChessExerciseManagement.Pieces;

namespace ChessExerciseManagement.Models {
    public class Player {
        public PlayerAffiliation Affiliation {
            private set;
            get;
        }

        public Board Board {
            private set;
            get;
        }

        public List<Piece> Pieces {
            private set;
            get;
        } = new List<Piece>();

        public List<Piece> LostPieces {
            private set;
            get;
        } = new List<Piece>();

        private static int ID;
        private int m_id {
            set;
            get;
        }

        public Player(PlayerAffiliation affiliation, Board board) {
            m_id = ID++;
            Affiliation = affiliation;
            Board = board;
        }

        public bool AddPiece(Piece newPiece) {
            if (newPiece.Affiliation != Affiliation) {
                return false;
            }

            Pieces.Add(newPiece);

            return true;
        }

        public void NotifyCapturedPiece(Piece lostPiece, Piece capturingPiece) {
            Pieces.Remove(lostPiece);
            LostPieces.Add(lostPiece);
        }

        public List<Field> GetAccessibleFields() {
            var list = new List<Field>();

            foreach (var piece in Pieces) {
                list.AddRange(piece.GetAccessibleFields());
            }

            return list;
        }

        public bool Equals(Player other) {
            return other.m_id == m_id;
        }

        public override bool Equals(object obj) {
            if (!(obj is Player)) {
                return false;
            }

            return Equals(obj as Player);
        }

        public override int GetHashCode() {
            return m_id;
        }
    }
}
