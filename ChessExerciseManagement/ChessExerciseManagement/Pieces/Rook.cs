using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class Rook : Piece {
        public Rook(Player player) : base(player) {
        }

        public override List<Field> GetAccessibleFields(Board board) {
            return Moves.GetAccessibleFieldsRook(board, this);
        }
    }
}
