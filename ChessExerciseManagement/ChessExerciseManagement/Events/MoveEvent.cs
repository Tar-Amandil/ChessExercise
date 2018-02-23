using System;
using ChessExerciseManagement.Models.Moves;

namespace ChessExerciseManagement.Events {
    public class MoveEvent : EventArgs {
        public Move Move {
            private set;
            get;
        }

        public MoveEvent(Move move) {
            if (move == null) {
                throw new ArgumentNullException("oldField must not be null");
            }

            Move = move;
        }
    }
}
