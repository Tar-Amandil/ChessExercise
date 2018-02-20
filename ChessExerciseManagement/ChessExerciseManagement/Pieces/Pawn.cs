using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class Pawn : Piece {
        public Pawn(Player player) : base(player) {
        }

        public override List<Field> GetAccessibleFields(Board board) {
            int dY = 0;

            if (Affiliation == PlayerAffiliation.Black) {
                dY = 1;
            } else if (Affiliation == PlayerAffiliation.White) {
                dY = -1;
            }

            return Moves.GetAccessibleFieldsPawn(board, this, dY);
        }
    }
}
