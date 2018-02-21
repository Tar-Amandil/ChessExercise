using System.Collections.Generic;
using System.Windows.Media.Imaging;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class Knight : Piece {
        public Knight(Player player, Board board, Field field) : base(player, board, field) {
        }

        public override List<Field> GetAccessibleFields() {
            return Moves.GetAccessibleFieldsKnight(Board, this);
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

        public override BitmapImage GetImage() {
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
