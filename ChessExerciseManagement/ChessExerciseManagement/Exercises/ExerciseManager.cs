using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ChessExerciseManagement.Exercises {
    public static class ExerciseManager {
        private static Dictionary<string, List<string>> m_exercises;
        public static Dictionary<string, List<string>> Exercises {
            set {
                if (value != null) {
                    m_exercises = value;
                }
            }
            get {
                return m_exercises;
            }
        }

        public static List<string> Keys {
            get {
                var keys = m_exercises.Keys.ToList();
                keys.RemoveAll(x => x.Length == 0);
                return keys;
            }
        }

        public static IEnumerable<string> Filter(List<string> keywords) {
            if (keywords == null) {
                return new List<string>();
            }

            IEnumerable<string> list = m_exercises[string.Empty];

            foreach (var keyword in keywords) {
                if (!m_exercises.ContainsKey(keyword)) {
                    return new List<string>();
                }

                var annotatedList = m_exercises[keyword];
                list = list.Where(l => annotatedList.Contains(l));
            }

            return list;
        }

        public static void AddExercise(string path, List<string> keywords) {
            var alreayExisting = m_exercises[string.Empty];
            if (path == null || !File.Exists(path) || alreayExisting.Contains(path)) {
                return;
            }

            alreayExisting.Add(path);

            foreach (var keyword in keywords) {
                if (!m_exercises.ContainsKey(keyword)) {
                    m_exercises.Add(keyword, new List<string>());
                }

                var list = m_exercises[keyword];
                list.Add(path);
            }
        }
    }
}
