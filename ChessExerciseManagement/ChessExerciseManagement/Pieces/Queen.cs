using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class Queen : Piece {
        public Queen(Player player) : base(player) {
        }

        public override List<Field> GetAccessibleFields(Board board) {
            var list = Moves.GetAccessibleFieldsBishop(board, this);
            list.AddRange(Moves.GetAccessibleFieldsRook(board, this));
            return list;
        }
    }
}
