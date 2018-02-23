using System.Linq;
using System.Collections.Generic;
using ChessExerciseManagement.Models.Pieces;
using ChessExerciseManagement.Events;
using ChessExerciseManagement.Base;
using System;
using ChessExerciseManagement.Models.Moves;

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

        public event PlayerMoveEventHandler Move;
        public delegate void PlayerMoveEventHandler(object sender, MoveEvent m);

        public event PlayerCaptureEventHandler Capture;
        public delegate void PlayerCaptureEventHandler(object sender, CaptureEvent m);

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

            newPiece.Move += Piece_Move;
            newPiece.Capture += Piece_Capture;

            return true;
        }

        private void Piece_Move(object sender, MoveEvent e) {
            var piece = sender as Piece;
            var move = e.Move;
            if (piece?.MoveCounter == 0) {
                if (piece is King) {
                    MayCastleShort = false;
                    MayCastleLong = false;

                    var y = move.NewField.Y;
                    var nX = move.NewField.X;
                    var oX = move.OldField.X;

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

            OnMove(e);
        }

        private void Piece_Capture(object sender, CaptureEvent e) {
            OnCapture(e);
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

        private void OnMove(MoveEvent e) {
            Move?.Invoke(this, e);
        }

        private void OnCapture(CaptureEvent e) {
            Capture?.Invoke(this, e);
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
