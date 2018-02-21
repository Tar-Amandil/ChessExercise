﻿using System;

using ChessExerciseManagement.Pieces;
using ChessExerciseManagement.UI;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Events;

namespace ChessExerciseManagement.Models {
    public class Field : BaseClass {
        public int X {
            private set;
            get;
        }

        public int Y {
            private set;
            get;
        }

        private Piece m_piece;
        public Piece Piece {
            set {
                if (m_piece?.Affiliation == value?.Affiliation) {
                    throw new ArgumentException("Those pieces cannot capture one another.");
                }

                if (value != null) {
                    m_piece?.Capture(value);
                }
                m_piece = value;

                OnPieceChange(value);
            }
            get {
                return m_piece;
            }
        }

        public event CustomFieldEventHandler PieceChange;
        public delegate void CustomFieldEventHandler(object sender, PieceEvent e);

        public FieldControl FieldControl {
            set;
            get;
        }

        public Field(int x, int y) {
            X = x;
            Y = y;
        }

        public void OnPieceChange(Piece piece) {
            PieceChange?.Invoke(this, new PieceEvent(piece));
        }
    }
}
