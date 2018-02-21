using System.Collections.Generic;
using System.Windows.Media.Imaging;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class Queen : Piece {
        public Queen(Player player, Board board, Field field) : base(player, board, field) {
        }

        public override List<Field> GetAccessibleFields() {
            var list = Moves.GetAccessibleFieldsBishop(Board, this);
            list.AddRange(Moves.GetAccessibleFieldsRook(Board, this));
            return list;
        }

        public override char GetFenChar() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return 'q';
                case PlayerAffiliation.White:
                    return 'Q';
            }

            return 'X';
        }

        public override BitmapImage GetImage() {
            switch (Affiliation) {
                case PlayerAffiliation.Black:
                    return images[6];
                case PlayerAffiliation.White:
                    return images[7];
            }

            return null;
        }
    }
}
