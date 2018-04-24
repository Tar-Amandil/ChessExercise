using System.Collections.Generic;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Bishop : Piece {
        public Bishop(Player player, Board board, Field field) : base(player, board, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'b' : 'B';
        }

        public override List<Field> GetAccessibleFields() {
            return MoveController.GetAccessibleFieldsBishop(Board, this);
        }
    }
}
