using System;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models.Moves {
    public class CaptureMove : Move {
        public Piece CapturedPiece {
            private set;
            get;
        }

        public CaptureMove(Field oldField, Field newField, Piece movedPiece, Piece capturedPiece)
            : this(oldField, newField, movedPiece, capturedPiece, false) {

        }

        public CaptureMove(Field oldField, Field newField, Piece movedPiece, Piece capturedPiece, bool check)
            : this(oldField, newField, movedPiece, capturedPiece, check, false) {

        }

        public CaptureMove(Field oldField, Field newField, Piece movedPiece, Piece capturedPiece, bool check, bool mate)
            : base(oldField, newField, movedPiece, check, mate) {
            if (capturedPiece == null) {
                throw new ArgumentNullException("capturedPiece must not be null");
            }

            CapturedPiece = capturedPiece;
        }

        public override string ToString() {
            var str = string.Empty;

            var c = char.ToUpper(MovedPiece.GetFenChar());
            if (c != 'P') {
                str += c;
            }

            var nX = NewField.X;
            var nY = NewField.Y;
            var oX = OldField.X;

            if (CapturedPiece != null) {
                if (c == 'P') {
                    str += c.Load(oX);
                }
                str += "x";
            }

            str += c.Load(nX);
            str += (nY + 1);

            if (Mate) {
                str += "#";
                return str;
            }

            if (Check) {
                str += "+";
                return str;
            }

            return str;
        }
    }
}
