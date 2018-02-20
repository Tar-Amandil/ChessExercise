using System;
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

        public override BitmapImage GetImage() {
            var path = @"\Images\Rook";

            switch(Affiliation) {
                case PlayerAffiliation.Black:
                    path += "Black.png";
                    break;
                case PlayerAffiliation.White:
                    path += "White.png";
                    break;
            }

            return new BitmapImage(new Uri(path, UriKind.Relative));
        }
    }
}
