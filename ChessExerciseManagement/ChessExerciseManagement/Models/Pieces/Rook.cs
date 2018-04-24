using System.Collections.Generic;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Rook : Piece {
        public Rook(Player player, Board board, Field field) : base(player, board, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'r' : 'R';
        }

        public override List<Field> GetAccessibleFields() {
            return MoveController.GetAccessibleFieldsRook(Board, this);
        }
    }
}
