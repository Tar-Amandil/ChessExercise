using System.Linq;
using System.Collections.Generic;

using ChessExerciseManagement.Pieces;
using ChessExerciseManagement.Events;
using ChessExerciseManagement.Base;
using System;

namespace ChessExerciseManagement.Models {
    public class Player : BaseClass {
        public PlayerAffiliation Affiliation {
            private set;
            get;
        }

        public List<Piece> Pieces {
            private set;
            get;
        } = new List<Piece>();

        public List<Piece> LostPieces {
            private set;
            get;
        } = new List<Piece>();

        public bool MayCastleShort {
            set;
            get;
        } = true;

        public bool MayCastleLong {
            set;
            get;
        } = true;

        private Board m_board;

        public event PlayerEventHandler AfterMove;
        public event PlayerEventHandler BeforeMove;
        public delegate void PlayerEventHandler(Player player, Move m);

        private bool inCheck;

        public Player(Board board, PlayerAffiliation affiliation) {
            m_board = board;
            Affiliation = affiliation;
        }

        public bool AddPiece(Piece newPiece) {
            if (newPiece.Affiliation != Affiliation) {
                return false;
            }

            Pieces.Add(newPiece);

            newPiece.BeforeMove += Piece_BeforeMove;
            newPiece.AfterMove += Piece_AfterMove;

            return true;
        }

        private void Piece_BeforeMove(Piece piece, MoveEvent e) {
            if (piece?.MoveCounter == 0) {
                if (piece is King) {
                    MayCastleShort = false;
                    MayCastleLong = false;

                    var y = e.NewField.Y;
                    var nX = e.NewField.X;
                    var oX = e.OldField.X;

                    if (oX - nX == 2) {
                        var rook = Pieces.Where(p => p.MoveCounter == 0 && p is Rook && p.Field.X == 0).First();
                        rook.SetField(m_board.Fields[oX - 1, y]);
                    } else if (oX - nX == -2) {
                        var rook = Pieces.Where(p => p.MoveCounter == 0 && p is Rook && p.Field.X == 7).First();
                        rook.SetField(m_board.Fields[oX + 1, y]);
                    }
                } else if (piece is Rook) {
                    switch (piece.Field.X) {
                        case 0:
                            MayCastleLong = false;
                            break;
                        case 7:
                            MayCastleShort = false;
                            break;
                    }
                }
            }

            OnBeforeMove(e, piece);
        }

        private void Piece_AfterMove(Piece sender, MoveEvent e) {
            OnAfterMove(e, sender);
        }

        public void NotifyCapturedPiece(Piece lostPiece, Piece capturingPiece) {
            Pieces.Remove(lostPiece);
            LostPieces.Add(lostPiece);
        }

        public List<Field> GetAccessibleFields(bool onlyAttacked) {
            var list = new List<Field>();

            foreach (var piece in Pieces) {
                if (onlyAttacked && piece is King) {
                    continue;
                }
                list.AddRange(piece.GetAccessibleFields());
            }

            return list;
        }

        internal void Uncheck() {
            inCheck = false;
        }

        internal void Check() {
            inCheck = true;
        }

        private void OnBeforeMove(MoveEvent e, Piece movedPiece) {
            BeforeMove?.Invoke(this, new Move(e.OldField, e.NewField, movedPiece, e.CapturedPiece));
        }

        private void OnAfterMove(MoveEvent e, Piece movedPiece) {
            AfterMove?.Invoke(this, new Move(e.OldField, e.NewField, movedPiece, e.CapturedPiece));
        }

        public string GetFenCastle() {
            var mes = string.Empty;

            if (MayCastleLong) {
                mes += Affiliation == PlayerAffiliation.White ? 'Q' : 'q';
            }
            if (MayCastleShort) {
                mes += Affiliation == PlayerAffiliation.White ? 'K' : 'k';
            }

            return mes;
        }
    }
}
