using System.Collections.Generic;
using System.Windows.Media.Imaging;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Models.Pieces {
    public class Bishop : Piece {
        public Bishop(Player player, Board board, Field field) : base(player, board, field) {
        }

        public override List<Field> GetAccessibleFields() {
            return MoveController.GetAccessibleFieldsBishop(Board, this);
        }

        public override char GetFenChar() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return 'b';
                case PlayerAffiliation.White:
                    return 'B';
            }

            return 'X';
        }

        public override BitmapImage GetImage() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return images[4];
                case PlayerAffiliation.White:
                    return images[5];
            }

            return null;
        }
    }
}
