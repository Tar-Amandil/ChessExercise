using System;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models.Moves {
    public class CastleMove : Move {
        public bool KingSide {
            private set;
            get;
        }

        public Field OldFieldRook {
            private set;
            get;
        }

        public Field NewFieldRook {
            private set;
            get;
        }

        public Piece Rook {
            private set;
            get;
        }

        public CastleMove(Field oldFieldRook, Field newFieldRook, Piece rook, Field oldFieldKing, Field newFieldKing, Piece king)
            : this(oldFieldRook, newFieldRook, rook, oldFieldKing, newFieldKing, king, false) {

        }

        public CastleMove(Field oldFieldRook, Field newFieldRook, Piece rook, Field oldFieldKing, Field newFieldKing, Piece king, bool check)
            : this(oldFieldRook, newFieldRook, rook, oldFieldKing, newFieldKing, king, check, false) {

        }

        public CastleMove(Field oldFieldRook, Field newFieldRook, Piece rook, Field oldFieldKing, Field newFieldKing, Piece king, bool check, bool mate)
            : base(oldFieldKing, newFieldKing, king, check, mate) {

            if (oldFieldRook == null) {
                throw new ArgumentNullException("oldFieldRook must not be null");
            }

            if (newFieldRook == null) {
                throw new ArgumentNullException("newFieldRook must not be null");
            }

            if (rook == null) {
                throw new ArgumentNullException("rook must not be null");
            }

            var nXR = newFieldRook.X;
            var oXR = oldFieldRook.X;

            KingSide = Math.Abs(nXR - oXR) == 2;

            OldFieldRook = oldFieldRook;
            NewFieldRook = newFieldRook;
            Rook = rook;
        }

        public override string ToString() {
            string str;
            if (KingSide) {
                str = "0-0";
            } else {
                str = "0-0-0";
            }

            if (Mate) {
                return str + "#";
            }

            if (Check) {
                return str + "+";
            }

            return str;
        }
    }
}
