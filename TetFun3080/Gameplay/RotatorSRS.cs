using Microsoft.Xna.Framework;
using TetFun3080.Backend;

namespace TetFun3080.Gameplay
{
    internal class RotatorSRS : IRotator
    {
        public bool KicksEnabled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Vector2 KickCheckLeft(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board)
        {
            throw new System.NotImplementedException();
        }

        public Vector2 KickCheckRight(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board)
        {
            throw new System.NotImplementedException();
        }

        public Vector2[] RotateClockwise(Vector2 pilotPiece, Vector2[] pieceOffset, Board board)
        {
            throw new System.NotImplementedException();
        }

        public Vector2[] RotateCounterClockwise(Vector2 pilotPiece, Vector2[] pieceOffset, Board board)
        {
            throw new System.NotImplementedException();
        }
    }
}
