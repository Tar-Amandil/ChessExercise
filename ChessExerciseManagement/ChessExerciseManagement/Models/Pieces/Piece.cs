using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using ChessExerciseManagement.Base;
using ChessExerciseManagement.Events;
using ChessExerciseManagement.Models.Moves;

namespace ChessExerciseManagement.Models.Pieces {
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

        public event MoveEventHandler Move;
        public delegate void MoveEventHandler(object sender, MoveEvent e);

        public event CaptureEventHandler Capture;
        public delegate void CaptureEventHandler(object sender, CaptureEvent e);

        public Piece(Player player, Board board, Field field) {
            if (player == null) {
                throw new ArgumentNullException("player must not be null.");
            }

            if (board == null) {
                throw new ArgumentNullException("board must not be null.");
            }

            if (field == null) {
                throw new ArgumentNullException("field must not be null.");
            }

            Player = player;
            Affiliation = player.Affiliation;
            Board = board;
            Field = field;
            field.Piece = this;
        }

        public void GetCaptured(Piece capturingPiece) {
            if (capturingPiece == null) {
                throw new ArgumentNullException("capturingPiece must not be null.");
            }

            Player.NotifyCapturedPiece(this, capturingPiece);
            Field = null;
        }

        public void SetField(Field field) {
            var oldField = Field;
            var newField = field;
            var capturedPiece = newField.Piece;

            if (capturedPiece == null) {
                OnMove(newField);
            } else {
                OnCapture(capturedPiece);
            }

            Field.Piece = null;
            Field = field;
            field.Piece = this;
            MoveCounter++;
        }

        private void OnMove(Field newField) {
            var move = new Move(Field, newField, this);
            var e = new MoveEvent(move);
            Move?.Invoke(this, e);
        }

        private void OnCapture(Piece capturedPiece) {
            var captureMove = new CaptureMove(Field, capturedPiece.Field, this, capturedPiece);
            var e = new CaptureEvent(captureMove);
            Capture?.Invoke(this, e);
        }

        public abstract List<Field> GetAccessibleFields();

        public abstract BitmapImage GetImage();

        public abstract char GetFenChar();
    }
}
