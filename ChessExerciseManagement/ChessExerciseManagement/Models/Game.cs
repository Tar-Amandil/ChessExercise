﻿using System.Text;
using System.Windows;
using ChessExerciseManagement.Pieces;

namespace ChessExerciseManagement.Models {
    public class Game {
        private PlayerAffiliation m_whosTurn = PlayerAffiliation.White;
        public PlayerAffiliation WhosTurn {
            get {
                return m_whosTurn;
            }
            set {
                if (m_whosTurn != value) {
                    m_whosTurn = value;
                    if (value == PlayerAffiliation.White) {
                        Movecounter++;
                    }
                }
            }
        }

        public Board Board {
            private set;
            get;
        }

        public Player White {
            private set;
            get;
        }

        public Player Black {
            private set;
            get;
        }

        public int HalfmovesSinceLastPawnmove {
            private set;
            get;
        }

        public int Movecounter {
            private set;
            get;
        } = 1;

        public Game() {
            Board = new Board(8, 8);
            White = new Player(PlayerAffiliation.White);
            Black = new Player(PlayerAffiliation.Black);

            Board.AddPlayer(White);
            Board.AddPlayer(Black);

            WhosTurn = PlayerAffiliation.White;

            AddWhitePieces();
            AddBlackPieces();
        }

        public Game(string fen) {
            Board = new Board(8, 8);
            White = new Player(PlayerAffiliation.White);
            Black = new Player(PlayerAffiliation.Black);

            Board.AddPlayer(White);
            Board.AddPlayer(Black);

            WhosTurn = PlayerAffiliation.White;

            var fenComps = fen.Split(' ');
            LoadPosition(fenComps[0]);
            MessageBox.Show(fen);
        }

        private void LoadPosition(string fen) {
            var ranks = fen.Split('/');
            var fieldcodes = new char[8, 8];

            for (var y = 0; y < 8; y++) {
                var pointer = 0;
                for (var x = 0; x < 8; x++) {

                    var c = ranks[7 - y][pointer];
                    byte b;

                    if (byte.TryParse(c.ToString(), out b)) {
                        var oldX = x;
                        for (var i = oldX; i < b + oldX; i++) {
                            fieldcodes[i, y] = '-';
                            x++;
                        }
                        x--;
                        pointer++;
                    } else {
                        fieldcodes[x, y] = c;
                        pointer++;
                    }
                }
            }

            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    var c = fieldcodes[x, y];
                    if (c == '-') {
                        continue;
                    }

                    var player = char.IsUpper(c) ? White : Black;
                    var field = Board.Fields[x, y];

                    Piece piece = null;
                    var tmpC = char.ToLower(c);
                    switch (tmpC) {
                        case 'r':
                            piece = new Rook(player, Board, field);
                            break;
                        case 'n':
                            piece = new Knight(player, Board, field);
                            break;
                        case 'b':
                            piece = new Bishop(player, Board, field);
                            break;
                        case 'q':
                            piece = new Queen(player, Board, field);
                            break;
                        case 'k':
                            piece = new King(player, Board, field);
                            break;
                        case 'p':
                            piece = new Pawn(player, Board, field);
                            break;
                    }

                    player.AddPiece(piece);
                }
            }


        }

        private void AddWhitePieces() {
            var fields = Board.Fields;

            var rook1 = new Rook(White, Board, fields[0, 0]);
            var rook2 = new Rook(White, Board, fields[7, 0]);

            var knight1 = new Knight(White, Board, fields[1, 0]);
            var knight2 = new Knight(White, Board, fields[6, 0]);

            var bishop1 = new Bishop(White, Board, fields[2, 0]);
            var bishop2 = new Bishop(White, Board, fields[5, 0]);

            var queen = new Queen(White, Board, fields[3, 0]);
            var king = new King(White, Board, fields[4, 0]);

            var pawn1 = new Pawn(White, Board, fields[0, 1]);
            var pawn2 = new Pawn(White, Board, fields[1, 1]);
            var pawn3 = new Pawn(White, Board, fields[2, 1]);
            var pawn4 = new Pawn(White, Board, fields[3, 1]);
            var pawn5 = new Pawn(White, Board, fields[4, 1]);
            var pawn6 = new Pawn(White, Board, fields[5, 1]);
            var pawn7 = new Pawn(White, Board, fields[6, 1]);
            var pawn8 = new Pawn(White, Board, fields[7, 1]);

            White.AddPiece(rook1);
            White.AddPiece(rook2);
            White.AddPiece(knight1);
            White.AddPiece(knight2);
            White.AddPiece(bishop1);
            White.AddPiece(bishop2);
            White.AddPiece(queen);
            White.AddPiece(king);
            White.AddPiece(pawn1);
            White.AddPiece(pawn2);
            White.AddPiece(pawn3);
            White.AddPiece(pawn4);
            White.AddPiece(pawn5);
            White.AddPiece(pawn6);
            White.AddPiece(pawn7);
            White.AddPiece(pawn8);
        }

        private void AddBlackPieces() {
            var fields = Board.Fields;

            var rook1 = new Rook(Black, Board, fields[0, 7]);
            var rook2 = new Rook(Black, Board, fields[7, 7]);

            var knight1 = new Knight(Black, Board, fields[1, 7]);
            var knight2 = new Knight(Black, Board, fields[6, 7]);

            var bishop1 = new Bishop(Black, Board, fields[2, 7]);
            var bishop2 = new Bishop(Black, Board, fields[5, 7]);

            var queen = new Queen(Black, Board, fields[3, 7]);
            var king = new King(Black, Board, fields[4, 7]);

            var pawn1 = new Pawn(Black, Board, fields[0, 6]);
            var pawn2 = new Pawn(Black, Board, fields[1, 6]);
            var pawn3 = new Pawn(Black, Board, fields[2, 6]);
            var pawn4 = new Pawn(Black, Board, fields[3, 6]);
            var pawn5 = new Pawn(Black, Board, fields[4, 6]);
            var pawn6 = new Pawn(Black, Board, fields[5, 6]);
            var pawn7 = new Pawn(Black, Board, fields[6, 6]);
            var pawn8 = new Pawn(Black, Board, fields[7, 6]);

            Black.AddPiece(rook1);
            Black.AddPiece(rook2);
            Black.AddPiece(knight1);
            Black.AddPiece(knight2);
            Black.AddPiece(bishop1);
            Black.AddPiece(bishop2);
            Black.AddPiece(queen);
            Black.AddPiece(king);
            Black.AddPiece(pawn1);
            Black.AddPiece(pawn2);
            Black.AddPiece(pawn3);
            Black.AddPiece(pawn4);
            Black.AddPiece(pawn5);
            Black.AddPiece(pawn6);
            Black.AddPiece(pawn7);
            Black.AddPiece(pawn8);
        }

        public string GetFen() {
            var sb = new StringBuilder();

            sb.Append(Board.GetFenCode());
            sb.Append(" ");

            sb.Append(WhosTurn == PlayerAffiliation.White ? 'w' : 'b');
            sb.Append(" ");

            var fenWhite = White.GetFenCastle();
            var fenBlack = Black.GetFenCastle();

            if (fenWhite == string.Empty && fenBlack == string.Empty) {
                sb.Append("-");
            } else {
                sb.Append(fenWhite);
                sb.Append(fenBlack);
            }
            sb.Append(" ");

            sb.Append("-");
            sb.Append(" ");

            sb.Append(HalfmovesSinceLastPawnmove);
            sb.Append(" ");

            sb.Append(Movecounter);
            sb.Append(" ");

            return sb.ToString();
        }
    }
}