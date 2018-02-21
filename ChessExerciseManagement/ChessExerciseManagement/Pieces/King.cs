﻿using System.Collections.Generic;
using System.Windows.Media.Imaging;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public class King : Piece {
        public King(Player player, Board board, Field field) : base(player, board, field) {
        }

        public override List<Field> GetAccessibleFields() {
            return Moves.GetAccessibleFieldsKing(Board, this);
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

        public override BitmapImage GetImage() {
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