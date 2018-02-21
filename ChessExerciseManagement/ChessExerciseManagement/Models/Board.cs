using System.Collections.Generic;
using System.Text;

namespace ChessExerciseManagement.Models {
    public class Board {
        public Field[,] Fields {
            private set;
            get;
        }

        public int Width {
            private set;
            get;
        }

        public int Height {
            private set;
            get;
        }

        public List<Player> Player {
            private set;
            get;
        } = new List<Player>();

        public Board(int width, int height) {
            Fields = new Field[width, height];

            Width = width;
            Height = height;

            for (var x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Fields[x, y] = new Field(this, x, y);
                }
            }
        }

        public void AddPlayer(Player player) {
            Player.Add(player);
        }

        public List<Field> GetAttackedFields(Player player, bool castle = false) {
            var list = new List<Field>();

            foreach (var p in Player) {
                if (p.Equals(player)) {
                    continue;
                }

                list.AddRange(p.GetAccessibleFields(castle));
            }

            return list;
        }

        public string GetFenCode() {
            var sb = new StringBuilder();

            for (var y = 7; y >= 0; y--) {
                var emptyFieldCounter = 0;

                for (int x = 0; x < 8; x++) {
                    var field = Fields[x, y];
                    var piece = field.Piece;

                    if (piece == null) {
                        emptyFieldCounter++;
                        continue;
                    }

                    if (emptyFieldCounter != 0) {
                        sb.Append(emptyFieldCounter);
                        emptyFieldCounter = 0;
                    }

                    sb.Append(piece.GetFenChar());
                }

                if(emptyFieldCounter != 0) {
                    sb.Append(emptyFieldCounter);
                }

                sb.Append('/');
            }
            return sb.ToString();
        }
    }
}
