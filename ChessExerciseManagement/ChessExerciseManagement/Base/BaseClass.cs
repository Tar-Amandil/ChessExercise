namespace ChessExerciseManagement.Base {
    public class BaseClass {
        private static int ID;
        private int m_id;

        public BaseClass() {
            m_id = ID++;
        }

        public bool Equals(BaseClass other) {
            return other.m_id == m_id;
        }

        public override bool Equals(object obj) {
            if (!(obj is BaseClass)) {
                return false;
            }

            return Equals(obj as BaseClass);
        }

        public override int GetHashCode() {
            return m_id;
        }
    }
}
