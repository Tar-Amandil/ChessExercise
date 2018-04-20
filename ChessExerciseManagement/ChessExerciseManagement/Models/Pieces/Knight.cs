using System.Collections.Generic;
using System.Drawing;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Knight : Piece {
        public Knight(Player player, Board board, Field field) : base(player, board, field) {
        }

        public override List<Field> GetAccessibleFields() {
            return MoveController.GetAccessibleFieldsKnight(Board, this);
        }

        public override char GetFenChar() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return 'n';
                case PlayerAffiliation.White:
                    return 'N';
            }

            return 'X';
        }

        public override Bitmap GetImage() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return images[2];
                case PlayerAffiliation.White:
                    return images[3];
            }

            return null;
        }
    }
}
