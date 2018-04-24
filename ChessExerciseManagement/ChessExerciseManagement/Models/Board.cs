using ChessExerciseManagement.Base;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChessExerciseManagement.Models {
    public class Board : BaseClass {
        private static Color whiteColor = Color.White;
        private static Color blackColor = Color.LightGray;

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

        public List<Field> GetAttackedFields(Player player, bool onlyAttacked) {
            var list = new List<Field>();

            foreach (var p in Player) {
                if (p.Equals(player)) {
                    continue;
                }

                list.AddRange(p.GetAccessibleFields(onlyAttacked));
            }

            return list;
        }

        private string GetFenCodeClassical() {
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

                if (emptyFieldCounter != 0) {
                    sb.Append(emptyFieldCounter);
                }
                if (y != 0) {
                    sb.Append('/');
                }
            }
            return sb.ToString();
        }

        private string GetFenCodeJonas() {
            var sb = new StringBuilder();

            var emptyFieldCounter = 0;
            for (var y = 7; y >= 0; y--) {
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
            }

            if (emptyFieldCounter != 0) {
                sb.Append(emptyFieldCounter);
            }

            return sb.ToString();
        }

        public string GetFenCode(FenMode mode) {
            switch (mode) {
                case FenMode.Classical:
                    return GetFenCodeClassical();
                case FenMode.Jonas:
                    return GetFenCodeJonas();
            }

            return string.Empty;
        }

        public Bitmap GetImage() {
            var images = GetImages();
            return MergePictures(images);
        }

        private Image[,] GetImages() {
            var images = new Image[8, 8];

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    var flag = i % 2 == j % 2;
                    var col = flag ? whiteColor : blackColor;
                    var field = Fields[i, j];

                    var image = new Bitmap(100, 100);

                    using (var graphics = Graphics.FromImage(image)) {
                        graphics.Clear(col);

                        var val = field.Piece?.GetBitmap();
                        if (val != null) {
                            graphics.DrawImage(val, new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, 200, 200), GraphicsUnit.Pixel);
                        }                        
                    }

                    images[i, j] = image;
                }
            }


            return images;
        }

        private Bitmap MergePictures(Image[,] images) {
            var outputBitmap = new Bitmap(800, 800, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(outputBitmap)) {
                for (var i = 0; i < 8; i++) {
                    for (var j = 0; j < 8; j++) {
                        var img = images[i, 7-j];
                        if (img == null) {
                            continue;
                        }

                        graphics.DrawImage(img, new PointF(100 * i, 100 * j));
                    }
                }
            }

            return outputBitmap;
        }
    }
}
