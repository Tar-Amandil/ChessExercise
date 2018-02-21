using System;
using System.Collections.Generic;

using ChessExerciseManagement.Models;
using System.Windows.Media.Imaging;

namespace ChessExerciseManagement.Pieces {
    public abstract class Piece {
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

        private static int ID;
        private int m_id {
            set;
            get;
        }

        public Piece(Player player, Board board, Field field) {
            if (player == null) {
                throw new ArgumentNullException("Player must not be null.");
            }
            m_id = ID++;
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
            if (this is King) {
                var oldX = Field.X;
                var newX = field.X;

                var dist = oldX - newX;

                if (dist == 2) {
                    var rook = Board.Fields[0, field.Y].Piece;
                    rook.SetField(Board.Fields[3, field.Y]);
                } else if (dist == -2) {
                    var rook = Board.Fields[7, field.Y].Piece;
                    rook.SetField(Board.Fields[5, field.Y]);
                }
            }

            Field.Piece = null;
            Field = field;
            field.Piece = this;
            if (count) {
                MoveCounter++;
            }
        }

        public abstract List<Field> GetAccessibleFields();

        public abstract BitmapImage GetImage();

        public abstract char GetFenChar();

        public bool Equals(Piece other) {
            return other.m_id == m_id;
        }

        public override bool Equals(object obj) {
            if (!(obj is Piece)) {
                return false;
            }

            return Equals(obj as Piece);
        }

        public override int GetHashCode() {
            return m_id;
        }
    }
}
