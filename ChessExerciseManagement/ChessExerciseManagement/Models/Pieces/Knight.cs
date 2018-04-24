using System.Collections.Generic;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Knight : Piece {
        public Knight(Player player, Board board, Field field) : base(player, board, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'n' : 'N';
        }

        public override List<Field> GetAccessibleFields() {
            return MoveController.GetAccessibleFieldsKnight(Board, this);
        }
    }
}
