using System.Collections.Generic;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Pawn : Piece {
        public Pawn(Player player, Board board, Field field) : base(player, board, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'p' : 'P';
        }

        public override List<Field> GetAccessibleFields() {
            int dY = 0;

            if (Affiliation == PlayerAffiliation.Black) {
                dY = -1;
            } else if (Affiliation == PlayerAffiliation.White) {
                dY = 1;
            }

            return MoveController.GetAccessibleFieldsPawn(Board, this, dY);
        }
    }
}
