﻿using System.Linq;
using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Pieces {
    public static class Moves {
        public static List<Field> GetAccessibleFieldsPawn(Board board, Piece piece, int dY) {
            var list = new List<Field>();

            var field = piece.Field;
            var affiliation = piece.Affiliation;

            var fields = board.Fields;
            var x = field.X;
            var y = field.Y;

            var nY = y + dY;

            if (nY >= 0 && nY < board.Height) {
                if (fields[x, nY].Piece == null) {
                    list.Add(fields[x, nY]);
                }

                var dX = x - 1;
                if (dX >= 0 && fields[dX, dY]?.Piece.Affiliation != affiliation) {
                    list.Add(fields[dX, dY]);
                }

                dX = x + 1;
                if (dX < board.Width && fields[dX, dY]?.Piece.Affiliation != affiliation) {
                    list.Add(fields[dX, dY]);
                }
            }

            var nnY = nY + dY;
            if (piece.MoveCounter == 0 && fields[x, nY].Piece == null && fields[x, nnY].Piece == null) {
                list.Add(fields[x, nnY]);
            }

            return list;
        }

        public static List<Field> GetAccessibleFieldsRook(Board board, Piece piece) {
            var list = new List<Field>();

            var field = piece.Field;
            var affiliation = piece.Affiliation;

            var fields = board.Fields;
            var x = field.X;
            var y = field.Y;

            for (var i = x + 1; i < board.Width; i++) {
                var nField = fields[i, y];
                var nPiece = nField.Piece;

                if (nPiece == null) {
                    list.Add(nField);
                    continue;
                }

                if (nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }

                break;
            }

            for (var i = x - 1; i >= 0; i--) {
                var nField = fields[i, y];
                var nPiece = nField.Piece;

                if (nPiece == null) {
                    list.Add(nField);
                    continue;
                }

                if (nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }

                break;
            }

            for (var i = y + 1; i < board.Height; i++) {
                var nField = fields[x, i];
                var nPiece = nField.Piece;

                if (nPiece == null) {
                    list.Add(nField);
                    continue;
                }

                if (nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }

                break;
            }

            for (var i = y - 1; i >= 0; i--) {
                var nField = fields[x, i];
                var nPiece = nField.Piece;

                if (nPiece == null) {
                    list.Add(nField);
                    continue;
                }

                if (nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }

                break;
            }

            return list;
        }

        public static List<Field> GetAccessibleFieldsKnight(Board board, Piece piece) {
            var list = new List<Field>();

            var field = piece.Field;
            var affiliation = piece.Affiliation;

            var fields = board.Fields;
            var x = field.X;
            var y = field.Y;

            var dX = new[] { 1, 2, 2, 1, -1, -2, -2, -1 };
            var dY = new[] { -2, -1, 1, 2, 2, 1, -1, -2 };

            for (var i = 0; i < 8; i++) {
                var nX = x + dX[i];
                var nY = y + dY[i];

                if (nX < 0 || nY < 0 || nX >= board.Width || nY >= board.Height) {
                    continue;
                }

                var nField = fields[nX, nY];
                var nPiece = nField.Piece;

                if (nPiece == null || nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }
            }

            return list;
        }

        public static List<Field> GetAccessibleFieldsBishop(Board board, Piece piece) {
            var list = new List<Field>();

            var field = piece.Field;
            var affiliation = piece.Affiliation;

            var fields = board.Fields;
            var x = field.X;
            var y = field.Y;

            for (int i = x + 1, j = y + 1; x < board.Width && y < board.Height; x++, y++) {
                var nField = fields[i, j];
                var nPiece = nField.Piece;

                if (nPiece == null) {
                    list.Add(nField);
                    continue;
                }

                if (nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }

                break;
            }

            for (int i = x - 1, j = y - 1; x >= 0 && y >= 0; x--, y--) {
                var nField = fields[i, j];
                var nPiece = nField.Piece;

                if (nPiece == null) {
                    list.Add(nField);
                    continue;
                }

                if (nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }

                break;
            }

            for (int i = x + 1, j = y - 1; x < board.Width && y >= 0; x++, y--) {
                var nField = fields[i, j];
                var nPiece = nField.Piece;

                if (nPiece == null) {
                    list.Add(nField);
                    continue;
                }

                if (nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }

                break;
            }

            for (int i = x - 1, j = y + 1; x >= 0 && y < board.Height; x--, y++) {
                var nField = fields[i, j];
                var nPiece = nField.Piece;

                if (nPiece == null) {
                    list.Add(nField);
                    continue;
                }

                if (nPiece.Affiliation != affiliation) {
                    list.Add(nField);
                }

                break;
            }

            return list;
        }

        public static List<Field> GetAccessibleFieldsKing(Board board, Piece piece) {
            var list = new List<Field>();

            var player = piece.Player;
            var field = piece.Field;
            var affiliation = piece.Affiliation;

            var fields = board.Fields;
            var x = field.X;
            var y = field.Y;

            var dX = new[] { 0, -1, -1, -1, 0, 1, 1, 1 };
            var dY = new[] { 1, 1, 0, -1, -1, -1, 0, 1 };

            var attackedFields = board.GetAttackedFields(player);

            for (var i = 0; i < 8; i++) {
                var nX = x + dX[i];
                var nY = y + dY[i];

                if (nX < 0 || nY < 0 || nX >= board.Width || nY >= board.Height) {
                    continue;
                }

                var nField = fields[nX, nY];
                var nPiece = nField.Piece;

                if (nPiece == null || nPiece.Affiliation != affiliation) {
                    if (attackedFields.Contains(nField)) {
                        continue;
                    }

                    list.Add(nField);
                }
            }

            if (piece.MoveCounter != 0) {
                return list;
            }

            var rooks = player.Pieces.Where(p => p is Rook && p.MoveCounter == 0);

            foreach (var rook in rooks) {
                var rX = rook.Field.X;
                var rY = rook.Field.Y;

                if (rX == 0) {
                    if (!(attackedFields.Contains(field)
                        || attackedFields.Contains(fields[x - 1, y])
                        || attackedFields.Contains(fields[x - 2, y]))) {
                        list.Add(fields[x - 2, y]);
                    }
                } else if (rX == board.Width - 1) {
                    if (!(attackedFields.Contains(field)
                        || attackedFields.Contains(fields[x + 1, y])
                        || attackedFields.Contains(fields[x + 2, y]))) {
                        list.Add(fields[x + 2, y]);
                    }
                }

            }

            return list;
        }
    }
}
