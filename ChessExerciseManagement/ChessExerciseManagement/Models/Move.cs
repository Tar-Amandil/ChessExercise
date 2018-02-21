using ChessExerciseManagement.Base;
using ChessExerciseManagement.Pieces;

namespace ChessExerciseManagement.Models {
    public class Move : BaseClass {
        public Field OldField {
            private set;
            get;
        }

        public Field NewField {
            private set;
            get;
        }

        public Piece MovedPiece {
            private set;
            get;
        }

        public Piece CapturedPiece {
            private set;
            get;
        }

        public bool Check;
        public bool Mate;

        public Move(Field oldField, Field newField, Piece movedPiece) : this(oldField, newField, movedPiece, null) {

        }

        public Move(Field oldField, Field newField, Piece movedPiece, Piece capturedPiece) {
            OldField = oldField;
            NewField = newField;
            MovedPiece = movedPiece;
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
                str += "++";
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
