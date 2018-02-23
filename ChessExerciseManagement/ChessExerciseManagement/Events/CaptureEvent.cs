using ChessExerciseManagement.Models.Moves;

namespace ChessExerciseManagement.Events {
    public class CaptureEvent : MoveEvent {
        public CaptureEvent(CaptureMove captureMove)
            : base(captureMove) {
        }
    }
}
