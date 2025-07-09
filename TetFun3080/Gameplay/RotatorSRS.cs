using Microsoft.Xna.Framework;
using TetFun3080.Backend;

namespace TetFun3080.Gameplay
{
    internal class RotatorSRS : IRotator
    {
        public int RotationState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public bool CheckValidity(Vector2 pilotPiece, Vector2[] pieceOffset, Vector2 overallOffset, Board board)
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
