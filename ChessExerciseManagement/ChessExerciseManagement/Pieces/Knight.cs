using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class Knight : Piece {
        public Knight(Player player) : base(player) {
        }

        public override List<Field> GetAccessibleFields(Board board) {
            return Moves.GetAccessibleFieldsKnight(board, this);
        }
    }
}
