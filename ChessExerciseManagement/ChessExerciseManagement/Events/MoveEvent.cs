using ChessExerciseManagement.Models;
using ChessExerciseManagement.Pieces;

namespace ChessExerciseManagement.Events {
    public class MoveEvent {
        public Field OldField {
            private set;
            get;
        }

        public Field NewField {
            private set;
            get;
        }

        public Piece CapturedPiece {
            private set;
            get;
        }

        public MoveEvent(Field oldField, Field newField, Piece capturedPiece) {
            OldField = oldField;
            NewField = newField;
            CapturedPiece = capturedPiece;
        }
    }
}
