using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Events {
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
