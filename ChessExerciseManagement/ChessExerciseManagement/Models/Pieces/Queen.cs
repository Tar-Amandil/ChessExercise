using System.Collections.Generic;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Queen : Piece {
        public Queen(Player player, Board board, Field field) : base(player, board, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'q' : 'Q';
        }

        public override List<Field> GetAccessibleFields() {
            var list = MoveController.GetAccessibleFieldsBishop(Board, this);
            list.AddRange(MoveController.GetAccessibleFieldsRook(Board, this));
            return list;
        }
    }
}
