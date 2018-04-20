using System.Collections.Generic;
using System.Drawing;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Rook : Piece {
        public Rook(Player player, Board board, Field field) : base(player, board, field) {
        }

        public override List<Field> GetAccessibleFields() {
            return MoveController.GetAccessibleFieldsRook(Board, this);
        }

        public override char GetFenChar() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return 'r';
                case PlayerAffiliation.White:
                    return 'R';
            }

            return 'X';
        }

        public override Bitmap GetImage() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return images[0];
                case PlayerAffiliation.White:
                    return images[1];
            }

            return null;
        }
    }
}
