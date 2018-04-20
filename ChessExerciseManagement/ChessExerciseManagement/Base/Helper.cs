using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace ChessExerciseManagement.Base {
    public static class Helper {
        public static BitmapImage Convert(Bitmap src) {
            if (src == null) {
                return null;
            }

            MemoryStream ms = new MemoryStream();
            src.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
