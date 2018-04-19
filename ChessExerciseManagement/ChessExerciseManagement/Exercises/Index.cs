using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChessExerciseManagement.Exercises {
    public static class Index {
        private static string m_folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ChessApplication";
        private static string m_filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ChessApplication\index.txt";
        private static string m_fenFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ChessApplication\Fen";
        private static string indexPath;

        public static string FolderPath {
            get {
                return m_folderPath;
            }
            set {
                if (Directory.Exists(value)) {
                    m_folderPath = value;
                }
            }
        }

        public static string FilePath {
            get {
                return m_filePath;
            }
            set {
                if (File.Exists(value)) {
                    m_filePath = value;
                }
            }
        }

        public static string FenFolderPath {
            get {
                return m_fenFolderPath;
            }
            set {
                if (Directory.Exists(value)) {
                    m_fenFolderPath = value;
                }
            }
        }

        public static void Load() {
            FindIndexPath();
            SaveIndexPath();
            var dict = ProcessIndex();

            ExerciseManager.Exercises = dict;
        }

        private static void FindIndexPath() {
            if (!Directory.Exists(m_folderPath)) {
                Directory.CreateDirectory(m_folderPath);
            }

            if (!File.Exists(m_filePath)) {
                File.Create(m_filePath).Close();
            }

            var content = File.ReadAllLines(m_filePath);
            indexPath = AppDomain.CurrentDomain.BaseDirectory + @"Exercises\index.dat";

            if (content.Length == 1) {
                var tmp = content[0];
                if (File.Exists(tmp)) {
                    if (tmp.EndsWith(".dat")) {
                        indexPath = tmp;
                    }
                }
            } else if (content.Length > 1) {
                throw new Exception("Your index file is corrupted. Please make sure the file: " + m_filePath
                    + " only contains a single line");
            }
        }

        private static void SaveIndexPath() {
            File.WriteAllText(m_filePath, indexPath);

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

        public static void SaveFens(List<string> fens, List<string> keywords) {
            if (!Directory.Exists(m_fenFolderPath)) {
                Directory.CreateDirectory(m_fenFolderPath);
            }

            var rnd = new Random();

            foreach (var fen in fens) {
                string filePath;
                do {
                    var num = rnd.Next();
                    filePath = m_fenFolderPath + "\\" + num.ToString();
                } while (File.Exists(filePath));

                File.WriteAllText(filePath, fen);
                AddFile(filePath, keywords);
                ExerciseManager.AddExercise(filePath, keywords);
            }
        }
    }
}
