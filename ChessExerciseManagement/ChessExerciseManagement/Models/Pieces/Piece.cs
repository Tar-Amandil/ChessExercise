﻿using System;
using System.Collections.Generic;

using ChessExerciseManagement.Base;
using ChessExerciseManagement.Events;
using ChessExerciseManagement.Models.Moves;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ChessExerciseManagement.Models.Pieces {
    public abstract class Piece : BaseClass {
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

        protected char m_key;

        static Piece() {
            var appPath = AppDomain.CurrentDomain.BaseDirectory + @"\Images\";

            PictureHelper.AddPicture(appPath + @"RookBlack.png", 'r');
            PictureHelper.AddPicture(appPath + @"RookWhite.png", 'R');
            PictureHelper.AddPicture(appPath + @"KnightBlack.png", 'n');
            PictureHelper.AddPicture(appPath + @"KnightWhite.png", 'N');
            PictureHelper.AddPicture(appPath + @"BishopBlack.png", 'b');
            PictureHelper.AddPicture(appPath + @"BishopWhite.png", 'B');
            PictureHelper.AddPicture(appPath + @"QueenBlack.png", 'q');
            PictureHelper.AddPicture(appPath + @"QueenWhite.png", 'Q');
            PictureHelper.AddPicture(appPath + @"KingBlack.png", 'k');
            PictureHelper.AddPicture(appPath + @"KingWhite.png", 'K');
            PictureHelper.AddPicture(appPath + @"PawnBlack.png", 'p');
            PictureHelper.AddPicture(appPath + @"PawnWhite.png", 'P');
        }

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

        public Bitmap GetBitmap() {
            return PictureHelper.GetPictureHelper(m_key).Bitmap;
        }

        public BitmapImage GetBitmapImage() {
            return PictureHelper.GetPictureHelper(m_key).BitmapImage;
        }

        public char GetFenChar() {
            return m_key;
        }
    }
}
