using System.Collections.Generic;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class King : Piece {
        public King(Player player, Board board, Field field) : base(player, board, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'k' : 'K';
        }

        public override List<Field> GetAccessibleFields() {
            return MoveController.GetAccessibleFieldsKing(Board, this);
        }
    }
}