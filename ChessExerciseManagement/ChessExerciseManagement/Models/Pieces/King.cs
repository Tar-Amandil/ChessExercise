using System.Collections.Generic;
using System.Drawing;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class King : Piece {
        public King(Player player, Board board, Field field) : base(player, board, field) {
        }

        public override List<Field> GetAccessibleFields() {
            return MoveController.GetAccessibleFieldsKing(Board, this);
        }

        public override char GetFenChar() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return 'k';
                case PlayerAffiliation.White:
                    return 'K';
            }

            return 'X';
        }

        public override Bitmap GetImage() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return images[8];
                case PlayerAffiliation.White:
                    return images[9];
            }

            return null;
        }
    }
}