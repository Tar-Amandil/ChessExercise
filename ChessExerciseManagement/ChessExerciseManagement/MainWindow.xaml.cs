using ChessExerciseManagement.Models;
using ChessExerciseManagement.Pieces;
using System.Windows;

namespace ChessExerciseManagement {
    public partial class MainWindow : Window {
        Board board;
        public static PlayerAffiliation WhosTurn = PlayerAffiliation.White;


        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            board = new Board(8, 8);
            var white = new Player(PlayerAffiliation.White, board);
            var black = new Player(PlayerAffiliation.Black, board);

            boardControl.SetBoard(board);

            AddWhitePieces(white, board);
            AddBlackPieces(black, board);
        }

        private void AddWhitePieces(Player player, Board board) {
            var fields = board.Fields;

            var rook1 = new Rook(player, board, fields[0, 0]);
            var rook2 = new Rook(player, board, fields[7, 0]);

            var knight1 = new Knight(player, board, fields[1, 0]);
            var knight2 = new Knight(player, board, fields[6, 0]);

            var bishop1 = new Bishop(player, board, fields[2, 0]);
            var bishop2 = new Bishop(player, board, fields[5, 0]);

            var queen = new Queen(player, board, fields[3, 0]);
            var king = new King(player, board, fields[4, 0]);

            var pawn1 = new Pawn(player, board, fields[0, 1]);
            var pawn2 = new Pawn(player, board, fields[1, 1]);
            var pawn3 = new Pawn(player, board, fields[2, 1]);
            var pawn4 = new Pawn(player, board, fields[3, 1]);
            var pawn5 = new Pawn(player, board, fields[4, 1]);
            var pawn6 = new Pawn(player, board, fields[5, 1]);
            var pawn7 = new Pawn(player, board, fields[6, 1]);
            var pawn8 = new Pawn(player, board, fields[7, 1]);

            player.AddPiece(rook1);
            player.AddPiece(rook2);
            player.AddPiece(knight1);
            player.AddPiece(knight2);
            player.AddPiece(bishop1);
            player.AddPiece(bishop2);
            player.AddPiece(queen);
            player.AddPiece(king);
            player.AddPiece(pawn1);
            player.AddPiece(pawn2);
            player.AddPiece(pawn3);
            player.AddPiece(pawn4);
            player.AddPiece(pawn5);
            player.AddPiece(pawn6);
            player.AddPiece(pawn7);
            player.AddPiece(pawn8);
        }

        private void AddBlackPieces(Player player, Board board) {
            var fields = board.Fields;

            var rook1 = new Rook(player, board, fields[0, 7]);
            var rook2 = new Rook(player, board, fields[7, 7]);

            var knight1 = new Knight(player, board, fields[1, 7]);
            var knight2 = new Knight(player, board, fields[6, 7]);

            var bishop1 = new Bishop(player, board, fields[2, 7]);
            var bishop2 = new Bishop(player, board, fields[5, 7]);

            var queen = new Queen(player, board, fields[3, 7]);
            var king = new King(player, board, fields[4, 7]);

            var pawn1 = new Pawn(player, board, fields[0, 6]);
            var pawn2 = new Pawn(player, board, fields[1, 6]);
            var pawn3 = new Pawn(player, board, fields[2, 6]);
            var pawn4 = new Pawn(player, board, fields[3, 6]);
            var pawn5 = new Pawn(player, board, fields[4, 6]);
            var pawn6 = new Pawn(player, board, fields[5, 6]);
            var pawn7 = new Pawn(player, board, fields[6, 6]);
            var pawn8 = new Pawn(player, board, fields[7, 6]);

            player.AddPiece(rook1);
            player.AddPiece(rook2);
            player.AddPiece(knight1);
            player.AddPiece(knight2);
            player.AddPiece(bishop1);
            player.AddPiece(bishop2);
            player.AddPiece(queen);
            player.AddPiece(king);
            player.AddPiece(pawn1);
            player.AddPiece(pawn2);
            player.AddPiece(pawn3);
            player.AddPiece(pawn4);
            player.AddPiece(pawn5);
            player.AddPiece(pawn6);
            player.AddPiece(pawn7);
            player.AddPiece(pawn8);
        }
    }
}
