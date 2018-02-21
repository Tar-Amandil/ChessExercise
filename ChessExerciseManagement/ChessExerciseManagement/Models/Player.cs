using System.Linq;
using System.Collections.Generic;

using ChessExerciseManagement.Pieces;

namespace ChessExerciseManagement.Models {
    public class Player {
        public PlayerAffiliation Affiliation {
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

        public Player(PlayerAffiliation affiliation) {
            m_id = ID++;
            Affiliation = affiliation;
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

        public List<Field> GetAccessibleFields(bool castle) {
            var list = new List<Field>();

            foreach (var piece in Pieces) {
                if (castle && piece is King) {
                    continue;
                }
                list.AddRange(piece.GetAccessibleFields());
            }

            return list;
        }

        public string GetFenCastle() {
            var relevantPieces = Pieces.Where(p => (p is King || p is Rook) && p.MoveCounter == 0);
            var kings = relevantPieces.Where(p => p is King);
            var rooks = relevantPieces.Where(p => p is Rook);

            if (kings.Count() != 1 || rooks.Count() == 0) {
                return string.Empty;
            }

            var mes = string.Empty;

            foreach (Rook rook in rooks) {
                if (rook.Field.X == 0) {
                    mes += Affiliation == PlayerAffiliation.White ? 'Q' : 'q';
                } else if (rook.Field.X == 7) {
                    mes += Affiliation == PlayerAffiliation.White ? 'K' : 'k';
                }
            }

            return mes;
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
