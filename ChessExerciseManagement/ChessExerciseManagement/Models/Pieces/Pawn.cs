using System.Collections.Generic;
using System.Drawing;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Pawn : Piece {
        public Pawn(Player player, Board board, Field field) : base(player, board, field) {
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

        public override char GetFenChar() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return 'p';
                case PlayerAffiliation.White:
                    return 'P';
            }

            return 'X';
        }

        public override Bitmap GetImage() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return images[10];
                case PlayerAffiliation.White:
                    return images[11];
            }

            return null;
        }
    }
}
