using System.Collections.Generic;

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
                    Fields[x, y] = new Field(x, y);
                }
            }
        }

        public void AddPlayer(Player player) {
            Player.Add(player);
        }

        public List<Field> GetAttackedFields(Player player) {
            var list = new List<Field>();

            foreach (var p in Player) {
                if (p.Equals(player)) {
                    continue;
                }

                list.AddRange(p.GetAccessibleFields());
            }

            return list;
        }
    }
}
