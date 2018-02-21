using System.Collections.Generic;
using System.Windows.Media.Imaging;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class Rook : Piece {
        public Rook(Player player, Board board, Field field) : base(player, board, field) {
        }

        public override List<Field> GetAccessibleFields() {
            return Moves.GetAccessibleFieldsRook(Board, this);
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

        public override BitmapImage GetImage() {
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
