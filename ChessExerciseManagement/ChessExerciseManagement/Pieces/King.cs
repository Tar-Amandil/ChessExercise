using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class King : Piece {
        public King(Player player) : base(player) {
        }

        public override List<Field> GetAccessibleFields(Board board) {
            return Moves.GetAccessibleFieldsKing(board, this);
        }
    }
}