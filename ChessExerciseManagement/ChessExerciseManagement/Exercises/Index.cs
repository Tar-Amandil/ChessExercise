using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChessExerciseManagement.Exercises {
    public static class Index {
        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ChessApplication";
        private static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ChessApplication\index.txt";
        private static string indexPath;

        public static void Load() {
            FindIndexPath();
            SaveIndexPath();
            var dict = ProcessIndex();

            ExerciseManager.Exercises = dict;
        }

        private static void FindIndexPath() {
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath)) {
                File.Create(filePath).Close();
            }

            var content = File.ReadAllLines(filePath);
            indexPath = AppDomain.CurrentDomain.BaseDirectory + @"Exercises\index.dat";

            if (content.Length == 1) {
                var tmp = content[0];
                if (File.Exists(tmp)) {
                    if (tmp.EndsWith(".dat")) {
                        indexPath = tmp;
                    }
                }
            } else if (content.Length > 1) {
                throw new Exception("Your index file is corrupted. Please make sure the file: " + filePath
                    + " only contains a single line");
            }
        }

        private static void SaveIndexPath() {
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ChessApplication\index.txt";
            File.WriteAllText(filePath, indexPath);

            if (!File.Exists(indexPath)) {
                var info = new FileInfo(indexPath);
                info.Directory.Create();
                info.Create().Close();
            }
        }

        private static Dictionary<string, List<string>> ProcessIndex() {
            var dict = new Dictionary<string, List<string>>();
            if (!File.Exists(indexPath)) {
                return dict;
            }

            dict.Add(string.Empty, new List<string>());

            var content = File.ReadAllLines(indexPath);

            foreach (var exercise in content) {
                var parts = exercise.Split(';');
                if (parts.Length == 0) {
                    continue;
                }

                var filePath = parts[0];
                if (!File.Exists(filePath)) {
                    continue;
                }

                var defaultList = dict[string.Empty];
                defaultList.Add(filePath);

                for (var i = 1; i < parts.Length; i++) {
                    var key = parts[i];

                    if (!dict.ContainsKey(key)) {
                        dict.Add(key, new List<string>());
                    }

                    var list = dict[key];
                    list.Add(filePath);
                }
            }

            return dict;
        }

        public static void AddFile(string path, List<string> keywords) {
            if (path == null || keywords == null || !File.Exists(path)) {
                return;
            }

            var sb = new StringBuilder();
            sb.Append(path);

            foreach (var keyword in keywords) {
                sb.Append(";");
                sb.Append(keyword);
            }
            sb.AppendLine();

            File.AppendAllText(indexPath, sb.ToString());
        }
    }
}
