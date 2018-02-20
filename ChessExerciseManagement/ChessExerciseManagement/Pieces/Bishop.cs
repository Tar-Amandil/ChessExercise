using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class Bishop : Piece {
        public Bishop(Player player) : base(player) {
        }

        public override List<Field> GetAccessibleFields(Board board) {
            return Moves.GetAccessibleFieldsBishop(board, this);
        }
    }
}
