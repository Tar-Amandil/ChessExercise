using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models;
using ChessExerciseManagement.Events;

namespace ChessExerciseManagement.Pieces {
    public abstract class Piece : BaseClass {
        protected static BitmapImage[] images = new BitmapImage[] {
            new BitmapImage(new Uri(@"\Images\RookBlack.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\RookWhite.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\KnightBlack.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\KnightWhite.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\BishopBlack.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\BishopWhite.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\QueenBlack.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\QueenWhite.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\KingBlack.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\KingWhite.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\PawnBlack.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"\Images\PawnWhite.png", UriKind.Relative)),
        };

        public PlayerAffiliation Affiliation {
            private set;
            get;
        }

        public Player Player {
            private set;
            get;
        }

        public Field Field {
            private set;
            get;
        }

        public Board Board {
            private set;
            get;
        }

        public int MoveCounter {
            private set;
            get;
        }

        public event PieceEventHandler AfterMove;
        public event PieceEventHandler BeforeMove;
        public delegate void PieceEventHandler(Piece sender, MoveEvent e);

        public Piece(Player player, Board board, Field field) {
            if (player == null) {
                throw new ArgumentNullException("Player must not be null.");
            }
            Player = player;
            Affiliation = player.Affiliation;
            Board = board;
            Field = field;
            field.Piece = this;
        }

        public void Capture(Piece capturingPiece) {
            Player.NotifyCapturedPiece(this, capturingPiece);
            Field = null;
        }

        public void SetField(Field field, bool count = true) {
            var oldField = Field;
            var newField = field;
            var capturedPiece = newField.Piece;

            OnBeforeMove(oldField, newField);

            Field.Piece = null;
            Field = field;
            field.Piece = this;
            if (count) {
                MoveCounter++;
            }

            OnAfterMove(oldField, newField, capturedPiece);
        }

        public abstract List<Field> GetAccessibleFields();

        public abstract BitmapImage GetImage();

        public abstract char GetFenChar();

        private void OnBeforeMove(Field oldField, Field newField) {
            BeforeMove?.Invoke(this, new MoveEvent(oldField, newField, newField.Piece));
        }

        private void OnAfterMove(Field oldField, Field newField, Piece capturedPiece) {
            AfterMove?.Invoke(this, new MoveEvent(oldField, newField, capturedPiece));
        }
    }
}
