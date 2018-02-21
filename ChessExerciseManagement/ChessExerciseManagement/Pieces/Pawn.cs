using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
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

            return Moves.GetAccessibleFieldsPawn(Board, this, dY);
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

        public override BitmapImage GetImage() {
            var path = @"\Images\Pawn";

            switch (Affiliation) {
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
