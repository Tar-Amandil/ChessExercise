using System;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models.Moves {
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

        public bool Check {
            private set;
            get;
        }

        public bool Mate {
            private set;
            get;
        }

        public Move(Field oldField, Field newField, Piece movedPiece)
            : this(oldField, newField, movedPiece, false) {

        }

        public Move(Field oldField, Field newField, Piece movedPiece, bool check)
            : this(oldField, newField, movedPiece, check, false) {

        }

        public Move(Field oldField, Field newField, Piece movedPiece, bool check, bool mate) {
            if (oldField == null) {
                throw new ArgumentNullException("oldField must not be null");
            }

            if (newField == null) {
                throw new ArgumentNullException("newField must not be null");
            }

            if (movedPiece == null) {
                throw new ArgumentNullException("movedPiece must not be null");
            }

            if (mate && !check) {
                throw new ArgumentException("There cannot be a mate if it is not check");
            }

            OldField = oldField;
            NewField = newField;
            MovedPiece = movedPiece;

            Check = check;
            Mate = mate;
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
