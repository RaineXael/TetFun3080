using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;

namespace TetFun3080
{
    internal class RotatorARS : IRotator
    {
        public bool KicksEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Vector2 KickCheckLeft(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board)
        {
            throw new NotImplementedException();
        }

        public Vector2 KickCheckRight(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board)
        {
            throw new NotImplementedException();
        }

        public Vector2[] RotateClockwise(Vector2 pilotPiece, Vector2[] pieceOffset, Board board)
        {
            throw new NotImplementedException();
        }

        public Vector2[] RotateCounterClockwise(Vector2 pilotPiece, Vector2[] pieceOffset, Board board)
        {
            throw new NotImplementedException();
        }
    }
}
