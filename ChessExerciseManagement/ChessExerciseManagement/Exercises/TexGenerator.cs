using System;
using System.IO;
using System.Object;
using System.Text.StringBuilder;

namespace ChessExerciseManagement.Exercises
{
    public static class TexGenerator
    {
        public static String GenerateTexFile(string path, string header, string task, string[] imagePaths, int positions)
        {
            try
            {
                if (File.Exists(path))
                {
                    //throw new FileAlreadyExistsException(path);
                }

                int rows, columns;
                double imageWidth;

                switch (positions)
                {
                    case 4:
                        rows = 2;
                        columns = 2;
                        imageWidth = 0.47;
                        break;
                    case 6:
                        rows = 3;
                        columns = 2;
                        imageWidth = 0.47;
                        break;
                    case 9:
                        rows = 3;
                        columns = 3;
                        imageWidth = 0.31;
                        break;
                    default:
                        //throw new UnknownPositionNumberException(positions);
                }

                using (FileStream fs = File.Create(path))
                {
                    StringBuilder sb = new StringBuilder();
                    sb = sb.AppendLine(@"\documentclass[10pt, a4paper]{article}")
                        .AppendLine(@"\usepackage[utf8]{inputenc}")
                        .AppendLine(@"\usepackage{graphicx}")
                        .AppendLine(@"\usepackage{subfigure}")
                        .AppendLine(@"\usepackage{amsmath}")
                        .AppendLine(@"\usepackage{amsfonts}")
                        .AppendLine(@"\usepackage{amssymb}")
                        .AppendLine(@"\usepackage[left = 2cm, right = 2cm, top = 2cm, bottom = 2cm]{geometry}")
                        .AppendLine(@"\begin{document}")
                        .AppendLine(@"\pagestyle{empty}")
                        .AppendLine(@"\textbf{\huge " + header + @"}\\")
                        .AppendLine(@"\\")
                        .AppendLine(@"\noindent \textit{\Large " + task + @"}\\");

                    for (int i=0; i < rows; i++)
                    {
                        sb = sb.AppendLine(@"\begin{figure}[h]");
                        for (int j=0; j < columns; j++)
                        {
                            sb = sb.AppendLine(@"\subfigure{\includegraphics[width = " + imageWidth + @"\textwidth]{" + imagePaths[j + i * columns] + @"}\hfill");
                        }
                        sb = sb.AppendLine(@"\end{figure}\\")
                            .AppendLine(@"\\ \\ \\ \\ \\");
                    }
                    sb = sb.AppendLine(@"\end{document}");
                    Byte[] info = new UTF8Encoding(true).GetBytes(sb.toString());
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return path;
        }
    }
}
