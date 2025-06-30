
using Microsoft.Xna.Framework;
using System;
using TetFun3080.Backend;

namespace TetFun3080
{
    internal class RotatorQuick : IRotator
    {
        public Vector2 KickCheckLeft(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board)
        {
            throw new NotImplementedException();
        }

        public Vector2 KickCheckRight(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board)
        {
            throw new NotImplementedException();
        }

        public Vector2[] RotateClockwise(Vector2[] pieceOffset, Board board)
        {
            Vector2[] result = pieceOffset;
            //(x, y) → (y, -x)
            for(int i = 0; i < 4; i++) {

                result[i] = new Vector2(pieceOffset[i].Y, -pieceOffset[i].X);
                
            }
            return result;
        }

        public Vector2[] RotateCounterClockwise(Vector2[] pieceOffset, Board board)
        {
            //(x, y) → (-y, x)
            Vector2[] result = pieceOffset;
          
            for (int i = 0; i < 4; i++)
            {

                result[i] = new Vector2(-pieceOffset[i].Y, pieceOffset[i].X);

            }
            return result;
        }
    }
}
